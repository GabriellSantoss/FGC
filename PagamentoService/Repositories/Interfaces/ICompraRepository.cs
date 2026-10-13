using PagamentoService.Model;

namespace PagamentoService.Repositories.Interfaces
{
    public interface ICompraRepository
    {
        Task<List<Compra>> BuscarTodosAsync();

        Task<Compra?> BuscarPorIdAsync(long id);

        Task AdicionarAsync(Compra compra);

        void Atualizar(Compra compra);

        void Remover(Compra compra);

        Task SalvarAlteracoesAsync();

        Task<bool> ExistePorPagamentoIdAsync(long pagamentoId);

        Task<List<Compra>> BuscarPorUsuarioAsync(Guid usuarioId);


    }
}