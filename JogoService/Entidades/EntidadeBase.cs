using System.ComponentModel.DataAnnotations.Schema;

namespace JogoService.Entidades
{
    public class EntidadeBase
    {
        public EntidadeBase(string? criadoPor = null) => CriadoPor = criadoPor ?? "";

        public long Id { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CriadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; } = null;

        public string AtualizadoPor { get; set; } = string.Empty;
    }
}
