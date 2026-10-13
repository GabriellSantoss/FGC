using Microsoft.EntityFrameworkCore;
using UsuarioService.Model;


namespace UsuarioService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Perfil> Perfis { get; set; } = null!;
        public DbSet<Permissao> Permissoes { get; set; } = null!;
        public DbSet<PerfilPermissao> PerfisPermissoes { get; set; } = null!;
    }
    }


