using UsuarioService.Entidades;

namespace UsuarioService.Model
{
    public class Perfil(string descricao, string criadoPor) : EntidadeBase(criadoPor)
    {
        public string Descricao { get; set; } = descricao;
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<PerfilPermissao> PerfilPermissoes { get; set; }
    }
}
