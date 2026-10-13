using PagamentoService.Requests;
using PagamentoService.Responses;

namespace PagamentoService.Services.Interfaces
{
    public interface ICompraService
    {
        Task<List<CompraResponse>> BuscarTodosAsync();

        Task<CompraResponse?> BuscarPorIdAsync(long id);

        Task<CompraResponse> CadastrarAsync(CompraRequest request);

        Task<CompraResponse?> AtualizarAsync(long id, CompraRequest request);

        Task<bool> ExcluirAsync(long id);

        Task<List<CompraResponse>> BuscarPorUsuarioAsync(Guid usuarioId);
    }
}