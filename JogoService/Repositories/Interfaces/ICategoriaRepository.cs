using JogoService.Model;

namespace JogoService.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> BuscarTodosAsync();
        Task<Categoria?> BuscarPorIdAsync(long id);
        Task AdicionarAsync(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Remover(Categoria categoria);
        Task SalvarAlteracoesAsync();
    }
}