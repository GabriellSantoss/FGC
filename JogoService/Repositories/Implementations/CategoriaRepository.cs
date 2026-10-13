using JogoService.Data;
using JogoService.Model;
using JogoService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JogoService.Repositories.Implementations
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> BuscarTodosAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> BuscarPorIdAsync(long id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Remover(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}