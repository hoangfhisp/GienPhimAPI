using GienPhim_Services.PaymentService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.PaymentService.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
        DbSet<PaymentMethod> PaymentMethod { get; set; } // Bảng phương thức thanh toán
        DbSet<PaymentTransaction> PaymentTransaction { get; set; } // Bảng giao dịch thanh toán

    }
}
