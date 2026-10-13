using UsuarioService.Requests;
using UsuarioService.Responses;

namespace UsuarioService.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponse>> BuscarTodosAsync();
        Task<UsuarioResponse?> BuscarPorIdAsync(long id);
        Task<UsuarioResponse> CadastrarAsync(UsuarioRequest request);
        Task<UsuarioResponse?> AtualizarAsync(long id, UsuarioRequest request);
        Task<bool> ExcluirAsync(long id);
    }
}