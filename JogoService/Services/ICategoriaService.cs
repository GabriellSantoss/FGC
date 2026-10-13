using JogoService.Requests;
using JogoService.Responses;

namespace JogoService.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaResponse>> BuscarTodosAsync();

        Task<CategoriaResponse?> BuscarPorIdAsync(long id);

        Task<CategoriaResponse> CadastrarAsync(CategoriaRequest request);

        Task<CategoriaResponse?> AtualizarAsync(
            long id,
            CategoriaRequest request);

        Task<bool> ExcluirAsync(long id);
    }
}