using GienPhim_Services.MembershipService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.MembershipService.Data
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
        
        DbSet<MembershipPlan> MembershipPlan { get; set; } // Bảng kế hoạch thành viên
        DbSet<PlanOption> PlanOption { get; set; } // Bảng tùy chọn kế hoạch thành viên
        DbSet<UserMembership> UserMembership { get; set; } // Bảng thành viên người dùng
    }
}
