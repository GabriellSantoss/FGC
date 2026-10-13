using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PagamentoService.Messaging.Events;
using PagamentoService.Repositories.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PagamentoService.Messaging
{
    public class PagamentoCriadoConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        private IConnection? _connection;
        private IChannel? _channel;

        public PagamentoCriadoConsumer(
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "rabbitmq",
                UserName = _configuration["RabbitMQ:User"] ?? "guest",
                Password = _configuration["RabbitMQ:Password"] ?? "guest"
            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync(
                cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: "pagamento-criado",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    var evento =
                        JsonSerializer.Deserialize<PagamentoCriadoEvent>(
                            json,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                    if (evento == null)
                    {
                        await _channel.BasicNackAsync(
                            eventArgs.DeliveryTag,
                            multiple: false,
                            requeue: false,
                            cancellationToken: stoppingToken);

                        return;
                    }

                    using var scope = _scopeFactory.CreateScope();

                    var pagamentoRepository =
                        scope.ServiceProvider
                            .GetRequiredService<IPagamentoRepository>();

                    var pagamento =
                        await pagamentoRepository.BuscarPorIdAsync(evento.Id);

                    if (pagamento != null)
                    {
                        pagamento.Status = "Aprovado";
                        pagamento.DataAtualizacao = DateTime.UtcNow;
                        pagamento.AtualizadoPor = "RabbitMQ Consumer";

                        pagamentoRepository.Atualizar(pagamento);
                        await pagamentoRepository.SalvarAlteracoesAsync();
                    }

                    Console.WriteLine(
                        $"Pagamento {evento.Id} processado pelo RabbitMQ.");

                    await _channel.BasicAckAsync(
                        eventArgs.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(
                        $"Erro ao processar pagamento: {exception.Message}");

                    await _channel.BasicNackAsync(
                        eventArgs.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        cancellationToken: stoppingToken);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: "pagamento-criado",
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            if (_channel != null)
                await _channel.CloseAsync(cancellationToken);

            if (_connection != null)
                await _connection.CloseAsync(cancellationToken);

            await base.StopAsync(cancellationToken);
        }
    }
}