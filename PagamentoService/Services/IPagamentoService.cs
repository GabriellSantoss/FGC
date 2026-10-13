using PagamentoService.Requests;
using PagamentoService.Responses;

namespace PagamentoService.Services.Interfaces
{
    public interface IPagamentoService
    {
        Task<List<PagamentoResponse>> BuscarTodosAsync();

        Task<PagamentoResponse?> BuscarPorIdAsync(long id);

        Task<PagamentoResponse> CadastrarAsync(PagamentoRequest request);

        Task<PagamentoResponse?> AtualizarStatusAsync(
            long id,
            string status);

        Task<bool> ExcluirAsync(long id);
    }
}