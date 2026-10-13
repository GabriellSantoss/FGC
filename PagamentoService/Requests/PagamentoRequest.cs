namespace PagamentoService.Requests
{
    public class PagamentoRequest
    {
        public Guid UsuarioId { get; set; }

        public long JogoId { get; set; }

        public decimal Valor { get; set; }

        public string FormaPagamento { get; set; } = string.Empty;
    }
}