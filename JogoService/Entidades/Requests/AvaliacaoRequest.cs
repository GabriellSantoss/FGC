using JogoService.Model;
using System.ComponentModel.DataAnnotations;

namespace JogoService.Entidades.Requests
{
    public class AvaliacaoRequest
    {
        [Range(1, long.MaxValue)]
        public long IdUsuario { get; set; }

        [Range(1, long.MaxValue)]
        public long IdJogo { get; set; }

        [Range(1, 5)]
        public int Nota { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comentario { get; set; } = string.Empty;


    }
}
