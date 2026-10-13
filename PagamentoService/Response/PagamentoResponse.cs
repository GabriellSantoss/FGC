namespace PagamentoService.Responses
{
    public class PagamentoResponse
    {
        public long Id { get; set; }

        public Guid UsuarioId { get; set; }

        public long JogoId { get; set; }

        public decimal Valor { get; set; }

        public string Status { get; set; } = string.Empty;

        public string FormaPagamento { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; }
    }
}