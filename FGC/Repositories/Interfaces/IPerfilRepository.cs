using UsuarioService.Model;

namespace UsuarioService.Repositories.Interfaces
{
    public interface IPerfilRepository
    {
        Task<List<Perfil>> BuscarTodosAsync();
        Task<Perfil?> BuscarPorIdAsync(long id);
        Task AdicionarAsync(Perfil perfil);
        void Atualizar(Perfil perfil);
        void Remover(Perfil perfil);
        Task SalvarAlteracoesAsync();
    }
}