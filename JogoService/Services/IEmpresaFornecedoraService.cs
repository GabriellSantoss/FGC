using JogoService.Requests;
using JogoService.Responses;

namespace JogoService.Services.Interfaces
{
    public interface IEmpresaFornecedoraService
    {
        Task<List<EmpresaFornecedoraResponse>> BuscarTodosAsync();

        Task<EmpresaFornecedoraResponse?> BuscarPorIdAsync(long id);

        Task<EmpresaFornecedoraResponse> CadastrarAsync(
            EmpresaFornecedoraRequest request);

        Task<EmpresaFornecedoraResponse?> AtualizarAsync(
            long id,
            EmpresaFornecedoraRequest request);

        Task<bool> ExcluirAsync(long id);
    }
}