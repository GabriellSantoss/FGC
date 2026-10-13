using Microsoft.EntityFrameworkCore;
using PagamentoService.Data;
using PagamentoService.Model;
using PagamentoService.Repositories.Interfaces;

namespace PagamentoService.Repositories.Implementations
{
    public class CompraRepository : ICompraRepository
    {
        private readonly ApplicationDbContext _context;

        public CompraRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Compra>> BuscarTodosAsync()
        {
            return await _context.Compras
                .Include(c => c.Pagamento)
                .ToListAsync();
        }

        public async Task<Compra?> BuscarPorIdAsync(long id)
        {
            return await _context.Compras
                .Include(c => c.Pagamento)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(Compra compra)
        {
            await _context.Compras.AddAsync(compra);
        }

        public void Atualizar(Compra compra)
        {
            _context.Compras.Update(compra);
        }

        public void Remover(Compra compra)
        {
            _context.Compras.Remove(compra);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistePorPagamentoIdAsync(long pagamentoId)
        {
            return await _context.Compras
                .AnyAsync(c => c.PagamentoId == pagamentoId);
        }

        public async Task<List<Compra>> BuscarPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.Compras
                .Include(c => c.Pagamento)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}