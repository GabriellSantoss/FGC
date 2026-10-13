namespace PagamentoService.Responses
{
    public class CompraResponse
    {
        public long Id { get; set; }

        public Guid UsuarioId { get; set; }

        public long JogoId { get; set; }

        public long PagamentoId { get; set; }

        public DateTime DataCompra { get; set; }
    }
}