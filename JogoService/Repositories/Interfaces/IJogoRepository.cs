using JogoService.Model;

namespace JogoService.Repositories.Interfaces
{
    public interface IJogoRepository
    {
        Task<List<Jogo>> BuscarTodosAsync();

        Task<Jogo?> BuscarPorIdAsync(long id);

        Task AdicionarAsync(Jogo jogo);

        void Atualizar(Jogo jogo);

        void Remover(Jogo jogo);

        Task SalvarAlteracoesAsync();
    }
}