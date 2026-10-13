namespace PagamentoService.Entidades
{
    public class EntidadeBase
    {
        public EntidadeBase(string? criadoPor = null)
        {
            CriadoPor = criadoPor ?? string.Empty;
        }

        public long Id { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public string CriadoPor { get; set; } = string.Empty;

        public DateTime? DataAtualizacao { get; set; }

        public string AtualizadoPor { get; set; } = string.Empty;
    }
}