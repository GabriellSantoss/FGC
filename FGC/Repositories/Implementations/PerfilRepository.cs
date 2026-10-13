using Microsoft.EntityFrameworkCore;
using UsuarioService.Data;
using UsuarioService.Model;
using UsuarioService.Repositories.Interfaces;

namespace UsuarioService.Repositories.Implementations
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly ApplicationDbContext _context;

        public PerfilRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Perfil>> BuscarTodosAsync()
        {
            return await _context.Perfis.ToListAsync();
        }

        public async Task<Perfil?> BuscarPorIdAsync(long id)
        {
            return await _context.Perfis.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AdicionarAsync(Perfil perfil)
        {
            await _context.Perfis.AddAsync(perfil);
        }

        public void Atualizar(Perfil perfil)
        {
            _context.Perfis.Update(perfil);
        }

        public void Remover(Perfil perfil)
        {
            _context.Perfis.Remove(perfil);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}