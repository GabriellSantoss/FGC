using UsuarioService.Entidades;

namespace UsuarioService.Model
{
    public class PerfilPermissao(long idPerfil, long idPermissao) : EntidadeBase
    {
        public long IdPerfil { get; set; } = idPerfil;
        public long IdPermissao { get; set; } = idPermissao;

        public Perfil Perfil { get; set; }
        public Permissao Permissao { get; set; }

    }
}
