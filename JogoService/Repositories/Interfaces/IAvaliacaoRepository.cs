using JogoService.Model;

namespace JogoService.Repositories.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<List<Avaliacao>> BuscarTodosAsync();
        Task<Avaliacao?> BuscarPorIdAsync(long id);
        Task AdicionarAsync(Avaliacao avaliacao);
        void Atualizar(Avaliacao avaliacao);
        void Remover(Avaliacao avaliacao);
        Task SalvarAlteracoesAsync();
    }
}
