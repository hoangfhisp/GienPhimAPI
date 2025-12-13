using GienPhim_Services.WalletService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.WalletService.Infrastructure.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.SoDu)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<WalletTransaction>(entity =>
            {
                entity.Property(e => e.SoDuSauGiaoDich)
                    .HasPrecision(18, 2);

                entity.Property(e => e.SoTienThayDoi)
                    .HasPrecision(18, 2);
            });

            base.OnModelCreating(modelBuilder);
            // Configure your entities here if needed
        }
        public DbSet<Wallet> Wallet { get; set; } // DbSet for Wallet entity
        public DbSet<WalletTransaction> WalletTransaction { get; set; } // DbSet for WalletTransaction entity
    }
}
