
using JogoService.Entidades;

namespace JogoService.Model
{
  
    public class Avaliacao(long idUsuario, long idJogo, int nota, string comentario, string criadoPor) : EntidadeBase(criadoPor)
    {
        public long IdUsuario { get; set; } = idUsuario;

        public long IdJogo { get; set; } = idJogo;
        public Jogo Jogo { get; set; }

        public int Nota { get; set; } = nota;

        public string Comentario { get; set; } = comentario;
    }
}
