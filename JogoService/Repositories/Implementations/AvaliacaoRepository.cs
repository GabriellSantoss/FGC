using JogoService.Data;
using JogoService.Model;
using JogoService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JogoService.Repositories.Implementations
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Avaliacao>> BuscarTodosAsync()
        {
            return await _context.Avaliacoes.ToListAsync();
        }

        public async Task<Avaliacao?> BuscarPorIdAsync(long id)
        {
            return await _context.Avaliacoes
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(Avaliacao avaliacao)
        {
            await _context.Avaliacoes.AddAsync(avaliacao);
        }

        public void Atualizar(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Update(avaliacao);
        }

        public void Remover(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Remove(avaliacao);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

