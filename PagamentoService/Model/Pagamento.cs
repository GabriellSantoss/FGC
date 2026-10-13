using PagamentoService.Entidades;

namespace PagamentoService.Model
{
    public class Pagamento : EntidadeBase
    {
        public Guid UsuarioId { get; set; }

        public long JogoId { get; set; }

        public decimal Valor { get; set; }

        public string Status { get; set; } = "Pendente";

        public string FormaPagamento { get; set; } = string.Empty;
    }
}