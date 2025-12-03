using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Infrastructure.Repositories
{
    public class ChatGroupRepository : IChatGroupRepository
    {
        private readonly InteractionDbContext _context;

        public ChatGroupRepository(InteractionDbContext context)
        {
            _context = context;
        }

        // Lấy 1 phòng chat theo Id
        public async Task<ChatGroup?> GetByIdAsync(Guid groupId, CancellationToken cancellationToken = default)
        {
            return await _context.ChatGroups
                .Include(g => g.Members)      // nếu cần đếm Member
                .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);
        }

        // Lấy list phòng theo phim (có phân trang + filter status)
        public async Task<(List<ChatGroup> groups, int total, int last)> GetByFilmAsync(int phimId,string? status,int page,int pageSize,CancellationToken cancellationToken = default)
        {
            var query = _context.ChatGroups
                .Include(g => g.Members)
                .Where(g => g.PhimId == phimId)
                .AsQueryable();

            // Lọc theo status (tuỳ quy ước của bạn)
            // Ví dụ:
            //  - active: chưa deleted + chưa hết hạn
            //  - closed: IsDeleted = true
            //  - expired: AutoDeleteAt <= nowUTC
            var now = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status.ToLower())
                {
                    case "active":
                        query = query.Where(g => !g.IsDeleted && g.AutoDeleteAt > now);
                        break;
                    case "closed":
                        query = query.Where(g => g.IsDeleted);
                        break;
                    case "expired":
                        query = query.Where(g => !g.IsDeleted && g.AutoDeleteAt <= now);
                        break;
                    default:
                        // "all" hoặc giá trị khác thì không filter thêm
                        break;
                }
            }

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / pageSize);

            var groups = await query
                .OrderByDescending(g => g.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (groups, total, last);
        }

        // Lấy list phòng của chính user (owner / member)
        public async Task<(List<ChatGroup> groups, int total, int last)> GetMineAsync(string userId,string? role,string? status,int page,int pageSize,CancellationToken cancellationToken = default)
        {
            // Base query: phòng có liên quan tới user
            var query = _context.ChatGroups
                .Include(g => g.Members)
                .Where(g =>
                    g.CreatedByUserId == userId ||
                    g.Members.Any(m => m.UserId == userId))
                .AsQueryable();

            // Filter role
            if (!string.IsNullOrWhiteSpace(role))
            {
                switch (role.ToLower())
                {
                    case "owner":
                        query = query.Where(g => g.CreatedByUserId == userId);
                        break;
                    case "member":
                        query = query.Where(g => g.Members.Any(m => m.UserId == userId && !m.IsOwner));
                        break;
                }
            }

            // Filter status giống trên
            var now = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status.ToLower())
                {
                    case "active":
                        query = query.Where(g => !g.IsDeleted && g.AutoDeleteAt > now);
                        break;
                    case "closed":
                        query = query.Where(g => g.IsDeleted);
                        break;
                    case "expired":
                        query = query.Where(g => !g.IsDeleted && g.AutoDeleteAt <= now);
                        break;
                }
            }

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / pageSize);

            var groups = await query
                .OrderByDescending(g => g.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (groups, total, last);
        }

        // Tạo mới phòng chat
        public async Task<ChatGroup> CreateAsync(ChatGroup group, CancellationToken cancellationToken = default)
        {
            _context.ChatGroups.Add(group);
            await _context.SaveChangesAsync(cancellationToken);
            return group;
        }

        // Cập nhật phòng chat (settings, invite code, close, ...)
        public async Task<ChatGroup> UpdateAsync(ChatGroup group, CancellationToken cancellationToken = default)
        {
            _context.ChatGroups.Update(group);
            await _context.SaveChangesAsync(cancellationToken);
            return group;
        }
    }
}
