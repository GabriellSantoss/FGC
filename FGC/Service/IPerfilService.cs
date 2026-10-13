using UsuarioService.Requests;
using UsuarioService.Responses;

namespace UsuarioService.Services.Interfaces
{
    public interface IPerfilService
    {
        Task<List<PerfilResponse>> BuscarTodosAsync();

        Task<PerfilResponse?> BuscarPorIdAsync(long id);

        Task<PerfilResponse> CadastrarAsync(PerfilRequest request);

        Task<PerfilResponse?> AtualizarAsync(long id, PerfilRequest request);

        Task<bool> ExcluirAsync(long id);
    }
}