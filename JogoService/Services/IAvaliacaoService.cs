using JogoService.Entidades.Requests;
using JogoService.Requests;
using JogoService.Responses;

namespace JogoService.Services
{
    public interface IAvaliacaoService
    {
        Task<List<AvaliacaoResponse>> BuscarTodosAsync();

        Task<AvaliacaoResponse?> BuscarPorIdAsync(long id);

        Task<AvaliacaoResponse> CadastrarAsync(AvaliacaoRequest request);

        Task<AvaliacaoResponse?> AtualizarAsync(
            long id,
            AvaliacaoRequest request);

        Task<bool> ExcluirAsync(long id);
    }
}
