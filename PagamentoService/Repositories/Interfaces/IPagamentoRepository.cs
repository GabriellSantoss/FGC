using PagamentoService.Model;

namespace PagamentoService.Repositories.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<List<Pagamento>> BuscarTodosAsync();

        Task<Pagamento?> BuscarPorIdAsync(long id);

        Task AdicionarAsync(Pagamento pagamento);

        void Atualizar(Pagamento pagamento);

        void Remover(Pagamento pagamento);

        Task SalvarAlteracoesAsync();
    }
}
