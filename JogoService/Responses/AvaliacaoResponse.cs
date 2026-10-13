using JogoService.Model;

namespace JogoService.Responses
{
    public class AvaliacaoResponse
    {
        public long Id { get; set; }

        public long IdUsuario { get; set; }

        public long IdJogo { get; set; } 
        public Jogo Jogo { get; set; }

        public int Nota { get; set; } 

        public string Comentario { get; set; } 
    }
}
