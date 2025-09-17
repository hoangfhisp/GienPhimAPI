using GienPhim_Services.InteractionService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Data
{
    // Dịch vụ tương tác
    public class InteractionDbContext : DbContext
    {
        public InteractionDbContext(DbContextOptions<InteractionDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

    }

}
