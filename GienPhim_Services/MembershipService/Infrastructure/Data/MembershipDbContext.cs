using GienPhim_Services.MembershipService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.MembershipService.Infrastructure.Data
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext(DbContextOptions<MembershipDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Có thể thêm cấu hình mô hình tại đây nếu cần
        }
        
        public DbSet<MembershipPlan> MembershipPlans { get; set; } // Bảng kế hoạch thành viên
        public DbSet<PlanOption> PlanOptions { get; set; } // Bảng tùy chọn kế hoạch thành viên
        public DbSet<UserMembership> UserMemberships { get; set; } // Bảng thành viên người dùng
    }
}
