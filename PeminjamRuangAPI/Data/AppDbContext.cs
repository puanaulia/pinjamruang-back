using Microsoft.EntityFrameworkCore;
using PeminjamRuangAPI.Entities;

namespace PeminjamRuangAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Peminjaman> Peminjaman { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set default status
            modelBuilder.Entity<Peminjaman>()
                .Property(p => p.Status)
                .HasDefaultValue("Menunggu");
        }
    }
}