namespace JogoService.Messaging.Events
{
    public class PagamentoAprovadoEvent
    {
        public long PagamentoId { get; set; }
        public Guid UsuarioId { get; set; }
        public long JogoId { get; set; }
    }
}