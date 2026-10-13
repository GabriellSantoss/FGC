using Microsoft.EntityFrameworkCore;
using UsuarioService.Data;
using UsuarioService.Model;
using UsuarioService.Repositories.Interfaces;

namespace UsuarioService.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> BuscarTodosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)
                .ToListAsync();
        }

        public async Task<Usuario?> BuscarPorIdAsync(long id)
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public void AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void RemoverAsync(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public async Task<bool> ExistePorEmailAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}