using UsuarioService.Model;

namespace UsuarioService.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> BuscarTodosAsync();

        Task<Usuario?> BuscarPorIdAsync(long id);

        Task<Usuario?> BuscarPorEmailAsync(string email);

        Task<bool> ExistePorEmailAsync(string email);

        Task AdicionarAsync(Usuario usuario);

        void AtualizarAsync(Usuario usuario);

        void RemoverAsync(Usuario usuario);

        Task SalvarAlteracoesAsync();
    }
}