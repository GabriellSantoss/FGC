using JogoService.Model;
using JogoService.Repositories.Implementations;

namespace JogoService.Repositories.Interfaces
{
    public interface IEmpresaFornecedoraRepository 
    {
        Task<List<EmpresaFornecedora>> BuscarTodosAsync();
        Task<EmpresaFornecedora?> BuscarPorIdAsync(long id);
        Task AdicionarAsync(EmpresaFornecedora empresaFornecedora);
        void Atualizar(EmpresaFornecedora empresaFornecedora);
        void Remover(EmpresaFornecedora empresaFornecedora);
        Task SalvarAlteracoesAsync();
    }
}
