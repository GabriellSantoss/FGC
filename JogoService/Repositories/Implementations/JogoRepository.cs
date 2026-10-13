using Microsoft.EntityFrameworkCore;
using JogoService.Data;
using JogoService.Model;
using JogoService.Repositories.Interfaces;

namespace JogoService.Repositories.Implementations
{
    public class JogoRepository : IJogoRepository
    {
        private readonly ApplicationDbContext _context;

        public JogoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Jogo>> BuscarTodosAsync()
        {
            return await _context.Jogos
                .Include(j => j.Categoria)
                .Include(j => j.EmpresaFornecedora)
                .ToListAsync();
        }

        public async Task<Jogo?> BuscarPorIdAsync(long id)
        {
            return await _context.Jogos
                .Include(j => j.Categoria)
                .Include(j => j.EmpresaFornecedora)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task AdicionarAsync(Jogo jogo)
        {
            await _context.Jogos.AddAsync(jogo);
        }

        public void Atualizar(Jogo jogo)
        {
            _context.Jogos.Update(jogo);
        }

        public void Remover(Jogo jogo)
        {
            _context.Jogos.Remove(jogo);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}