using Microsoft.EntityFrameworkCore;
using PagamentoService.Model;

namespace PagamentoService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pagamento> Pagamentos { get; set; } = null!;

        public DbSet<Compra> Compras { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Pagamento)
                .WithOne()
                .HasForeignKey<Compra>(c => c.PagamentoId);

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Valor)
                .HasPrecision(10, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}