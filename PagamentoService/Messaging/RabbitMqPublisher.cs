using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace PagamentoService.Messaging
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IConfiguration _configuration;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PublicarAsync<T>(string fila, T mensagem)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "rabbitmq",
                UserName = _configuration["RabbitMQ:User"] ?? "guest",
                Password = _configuration["RabbitMQ:Password"] ?? "guest"
            };

            await using var connection =
                await factory.CreateConnectionAsync();

            await using var channel =
                await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: fila,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var json = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: fila,
                mandatory: false,
                basicProperties: properties,
                body: body
            );
        }
    }
}