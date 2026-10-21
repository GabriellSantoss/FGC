namespace JogoService.Model
{
    public class Compra
    {
        public long Id { get; set; }
        public long PagamentoId { get; set; }
        public Guid UsuarioId { get; set; }
        public long JogoId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}