using GienPhim_Services.WalletService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.WalletService.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entities here if needed
        }
        DbSet<Wallet> Wallet { get; set; } // DbSet for Wallet entity
        DbSet<WalletTransaction> WalletTransaction { get; set; } // DbSet for WalletTransaction entity
    }
}
