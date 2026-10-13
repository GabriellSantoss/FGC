using UsuarioService.Entidades;

namespace UsuarioService.Model
{
    public class Permissao(string descricao, string criadoPor) : EntidadeBase(criadoPor)
    {
        public string Descricao { get; set; } = descricao;

        public ICollection<PerfilPermissao> PerfilPermissoes { get; set; } = [];
    }
}
