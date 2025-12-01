using GienPhim_Services.InteractionService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Infrastructure.Data
{
    public class InteractionDbContext : DbContext
    {
        public InteractionDbContext(DbContextOptions<InteractionDbContext> options)
            : base(options)
        {
        }

        // Chat
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }

        // Tương tác
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔗 1 ChatGroup có nhiều ChatMessages
            builder.Entity<ChatMessage>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 1 ChatGroup có nhiều GroupMembers
            builder.Entity<GroupMember>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 1 ChatGroup có nhiều JoinRequests
            builder.Entity<JoinRequest>()
                .HasOne(r => r.Group)
                .WithMany(g => g.JoinRequests)
                .HasForeignKey(r => r.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // ⚙️ Optional: Unique constraint tránh 1 user join 2 lần cùng group
            builder.Entity<GroupMember>()
                .HasIndex(m => new { m.GroupId, m.UserId })
                .IsUnique();

            // ⚙️ Optional: Unique constraint cho mỗi user chỉ gửi 1 request join
            builder.Entity<JoinRequest>()
                .HasIndex(r => new { r.GroupId, r.RequestUserId })
                .IsUnique();

            // ⚙️ Optional: Loại tin nhắn giới hạn độ dài
            builder.Entity<ChatMessage>()
                .Property(m => m.LoaiTinNhan)
                .HasMaxLength(50);
        }
    }
}
