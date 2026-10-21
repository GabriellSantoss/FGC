using System.Data;
using System.Text.Json;
using JogoService.Data;
using JogoService.Messaging.Events;
using JogoService.Model;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JogoService.Messaging
{
    public class PagamentoAprovadoConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PagamentoAprovadoConsumer> _logger;

        public PagamentoAprovadoConsumer(
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory,
            ILogger<PagamentoAprovadoConsumer> logger)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "rabbitmq",
                UserName = _configuration["RabbitMQ:User"] ?? "fcg",
                Password = _configuration["RabbitMQ:Password"] ?? "guest"
            };

            await using var connection =
                await factory.CreateConnectionAsync(stoppingToken);

            await using var channel =
                await connection.CreateChannelAsync(
                    cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(
                queue: "pagamento-aprovado",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            await channel.BasicQosAsync(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false,
                cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, args) =>
            {
                try
                {
                    var evento =
                        JsonSerializer.Deserialize<PagamentoAprovadoEvent>(
                            args.Body.ToArray(),
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                    if (evento == null ||
                        evento.PagamentoId <= 0 ||
                        evento.JogoId <= 0 ||
                        evento.UsuarioId == Guid.Empty)
                    {
                        throw new JsonException("Evento inválido.");
                    }

                    using var scope = _scopeFactory.CreateScope();

                    var context = scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContext>();

                    // Permite transação explícita com EnableRetryOnFailure.
                    var strategy = context.Database.CreateExecutionStrategy();

                    await strategy.ExecuteAsync(async () =>
                    {
                        context.ChangeTracker.Clear();

                        await using var transaction =
                            await context.Database.BeginTransactionAsync(
                                IsolationLevel.Serializable,
                                stoppingToken);

                        var jaRegistrado = await context.Compras
                            .AnyAsync(
                                c => c.PagamentoId == evento.PagamentoId,
                                stoppingToken);

                        if (!jaRegistrado)
                        {
                            var atualizados = await context.Jogos
                                .Where(j => j.Id == evento.JogoId)
                                .ExecuteUpdateAsync(
                                    setters => setters.SetProperty(
                                        j => j.NumeroVendas,
                                        j => j.NumeroVendas + 1),
                                    stoppingToken);

                            if (atualizados == 0)
                            {
                                throw new KeyNotFoundException(
                                    $"Jogo {evento.JogoId} não encontrado.");
                            }

                            context.Compras.Add(new Compra
                            {
                                PagamentoId = evento.PagamentoId,
                                UsuarioId = evento.UsuarioId,
                                JogoId = evento.JogoId
                            });

                            await context.SaveChangesAsync(stoppingToken);
                        }

                        await transaction.CommitAsync(stoppingToken);
                    });

                    await channel.BasicAckAsync(
                        args.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken);

                    _logger.LogInformation(
                        "Pagamento {PagamentoId}: compra confirmada no JogoService.",
                        evento.PagamentoId);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    // A mensagem sem confirmação volta à fila ao fechar a conexão.
                }
                catch (JsonException exception)
                {
                    _logger.LogError(
                        exception,
                        "Mensagem inválida em pagamento-aprovado.");

                    await channel.BasicNackAsync(
                        args.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        cancellationToken: stoppingToken);
                }

                catch (KeyNotFoundException exception)
                {
                    try
                    {
                        await channel.QueueDeclareAsync(
                            queue: "pagamento-aprovado-falhas",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null,
                            cancellationToken: stoppingToken);

                        await channel.BasicPublishAsync(
                            exchange: string.Empty,
                            routingKey: "pagamento-aprovado-falhas",
                            mandatory: true,
                            basicProperties: new BasicProperties
                            {
                                Persistent = true
                            },
                            body: args.Body.ToArray(),
                            cancellationToken: stoppingToken);

                        await channel.BasicAckAsync(
                            args.DeliveryTag,
                            multiple: false,
                            cancellationToken: stoppingToken);

                        _logger.LogError(
                            exception,
                            "Mensagem encaminhada para pagamento-aprovado-falhas.");
                    }
                    catch (Exception encaminhamentoException)
                    {
                        _logger.LogError(
                            encaminhamentoException,
                            "Falha ao encaminhar mensagem; será reenviada.");

                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                        await channel.BasicNackAsync(
                            args.DeliveryTag,
                            multiple: false,
                            requeue: true,
                            cancellationToken: stoppingToken);
                    }
                }

                catch (Exception exception)
                {
                    _logger.LogError(
                        exception,
                        "Falha ao registrar compra; mensagem será reenviada.");

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                    await channel.BasicNackAsync(
                        args.DeliveryTag,
                        multiple: false,
                        requeue: true,
                        cancellationToken: stoppingToken);
                }
            };

            await channel.BasicConsumeAsync(
                queue: "pagamento-aprovado",
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}