using JogoService.Entidades;

namespace JogoService.Model
{
    public class Categoria(string descricao, string criadoPor) : EntidadeBase(criadoPor)
    {
        public string Descricao { get; set; } = descricao;

        public ICollection<Jogo> Jogos { get; set; }

    }
}
