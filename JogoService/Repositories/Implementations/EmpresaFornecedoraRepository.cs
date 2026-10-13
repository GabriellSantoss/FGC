using JogoService.Data;
using JogoService.Model;
using JogoService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JogoService.Repositories.Implementations
{
    public class EmpresaFornecedoraRepository : IEmpresaFornecedoraRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpresaFornecedoraRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpresaFornecedora>> BuscarTodosAsync()
        {
            return await _context.EmpresasFornecedoras.ToListAsync();
        }

        public async Task<EmpresaFornecedora?> BuscarPorIdAsync(long id)
        {
            return await _context.EmpresasFornecedoras
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(EmpresaFornecedora empresaFornecedora)
        {
            await _context.EmpresasFornecedoras.AddAsync(empresaFornecedora);
        }

        public void Atualizar(EmpresaFornecedora empresaFornecedora)
        {
            _context.EmpresasFornecedoras.Update(empresaFornecedora);
        }

        public void Remover(EmpresaFornecedora empresaFornecedora)
        {
            _context.EmpresasFornecedoras.Remove(empresaFornecedora);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

