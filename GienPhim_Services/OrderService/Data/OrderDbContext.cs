using GienPhim_Services.OrderService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entities here if needed
        }
        public DbSet<Order> Order { get; set; } // DbSet for Order entity
        public DbSet<OrderDetail> OrderDetail { get; set; } // DbSet for OrderDetail entity
    }
}
