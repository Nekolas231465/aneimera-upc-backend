using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ponencia> Ponencias { get; set; }
        public DbSet<Taller> Talleres { get; set; }
        public DbSet<VisitaTecnica> VisitaTecnicas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ponencia>()
                .Property(p => p.Fecha)
                .HasColumnType("date");

            modelBuilder.Entity<Taller>()
                .Property(p => p.Fecha)
                .HasColumnType("date");

            modelBuilder.Entity<VisitaTecnica>()
                .Property(p => p.Fecha)
                .HasColumnType("date");
        }
    }
}
