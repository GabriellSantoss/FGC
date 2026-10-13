namespace PagamentoService.Messaging
{
    public interface IRabbitMqPublisher
    {
        Task PublicarAsync<T>(string fila, T mensagem);
    }
}