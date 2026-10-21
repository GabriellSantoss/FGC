using JogoService.Model;
using Microsoft.EntityFrameworkCore;

namespace JogoService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>()
                .HasOne(j => j.Categoria)
                .WithMany(c => c.Jogos)
                .HasForeignKey(j => j.IdCategoria);

            modelBuilder.Entity<Jogo>()
                .HasOne(j => j.EmpresaFornecedora)
                .WithMany(e => e.Jogos)
                .HasForeignKey(j => j.IdFornecedor);

            modelBuilder.Entity<Jogo>()
                .Property(j => j.Tamanho)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Avaliacao>()
            .HasOne(a => a.Jogo)
            .WithMany(j => j.Avaliacoes)
            .HasForeignKey(a => a.IdJogo);

            modelBuilder.Entity<Compra>()
            .HasIndex(c => c.PagamentoId)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Compra> Compras { get; set; } = null!;

        public DbSet<Jogo> Jogos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<EmpresaFornecedora> EmpresasFornecedoras { get; set; } = null!;
        public DbSet<Avaliacao> Avaliacoes { get; set; } = null!;
    }
}