namespace PagamentoService.Requests
{
    public class CompraRequest
    {
        public Guid UsuarioId { get; set; }

        public long JogoId { get; set; }

        public long PagamentoId { get; set; }
    }
}