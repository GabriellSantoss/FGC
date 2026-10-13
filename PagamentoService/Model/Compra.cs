using PagamentoService.Entidades;

namespace PagamentoService.Model
{
    public class Compra(
    Guid usuarioId,
    long jogoId,
    long pagamentoId,
    string criadoPor) : EntidadeBase(criadoPor) 
    {
        public Guid UsuarioId { get; set; } = usuarioId;
        public long JogoId { get; set; } = jogoId;
        public long PagamentoId { get; set; } = pagamentoId;
        public Pagamento Pagamento { get; set; } = null!;
        public DateTime DataCompra { get; set; } = DateTime.UtcNow;
    }
}