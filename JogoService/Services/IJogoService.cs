using JogoService.Requests;
using JogoService.Responses;

namespace JogoService.Services.Interfaces
{
    public interface IJogoService
    {
        Task<List<JogoResponse>> BuscarTodosAsync();

        Task<JogoResponse?> BuscarPorIdAsync(long id);

        Task<JogoResponse> CadastrarAsync(JogoRequest request);

        Task<JogoResponse?> AtualizarAsync(long id, JogoRequest request);

        Task<bool> ExcluirAsync(long id);
    }
}