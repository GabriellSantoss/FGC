using Microsoft.EntityFrameworkCore;
using PagamentoService.Data;
using PagamentoService.Model;
using PagamentoService.Repositories.Interfaces;

namespace PagamentoService.Repositories.Implementations
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pagamento>> BuscarTodosAsync()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        public async Task<Pagamento?> BuscarPorIdAsync(long id)
        {
            return await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AdicionarAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
        }

        public void Atualizar(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
        }

        public void Remover(Pagamento pagamento)
        {
            _context.Pagamentos.Remove(pagamento);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}