using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.MembershipService.Infrastructure.Repositories
{
    public class UserMembershipRepository : IUserMembershipRepository
    {
        private readonly MembershipDbContext _membershipContext;

        public UserMembershipRepository(MembershipDbContext membershipContext)
        {
            _membershipContext = membershipContext;
        }

        // Lấy tất cả UserMembership có phân trang
        public async Task<(List<UserMembership> userMemberships, int total, int last)> GetAllUserMembershipsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _membershipContext.UserMemberships
                .Include(um => um.PlanOption)
                .ThenInclude(po => po.MembershipPlan)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var userMemberships = await query
                .OrderBy(um => um.UserMembershipId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (userMemberships, total, last);
        }

        // Lấy chi tiết UserMembership theo Id
        public async Task<UserMembership?> GetByIdAsync(string userMembershipId, CancellationToken ct = default)
        {
            return await _membershipContext.UserMemberships
                .Include(um => um.PlanOption)
                .ThenInclude(po => po.MembershipPlan)
                .FirstOrDefaultAsync(um => um.UserMembershipId == userMembershipId, ct);
        }

        // Lấy danh sách UserMembership theo UserId (nếu bạn cần hỗ trợ tìm theo user)
        public async Task<List<UserMembership>> GetByUserIdAsync(string userId, CancellationToken ct = default)
        {
            return await _membershipContext.UserMemberships
                .Include(um => um.PlanOption)
                .ThenInclude(po => po.MembershipPlan)
                .Where(um => um.UserId == userId)
                .OrderByDescending(um => um.NgayBatDau)
                .ToListAsync(ct);
        }

        // Tạo mới UserMembership
        public async Task<UserMembership> CreateAsync(UserMembership userMembership, CancellationToken ct = default)
        {
            _membershipContext.UserMemberships.Add(userMembership);
            await _membershipContext.SaveChangesAsync(ct); // tự commit
            return userMembership;
        }

        // Cập nhật UserMembership
        public async Task<UserMembership> UpdateAsync(UserMembership userMembership, CancellationToken ct = default)
        {
            _membershipContext.UserMemberships.Update(userMembership);
            await _membershipContext.SaveChangesAsync(ct);
            return userMembership;
        }

        // Xóa UserMembership
        public async Task DeleteAsync(UserMembership userMembership, CancellationToken ct = default)
        {
            _membershipContext.UserMemberships.Remove(userMembership);
            await _membershipContext.SaveChangesAsync(ct);
        }

        // Kiểm tra trạng thái gói thành viên hiện tại của user
        public async Task<UserMembership?> GetCurrentMembershipAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _membershipContext.UserMemberships
                .Include(um => um.PlanOption)
                .ThenInclude(po => po.MembershipPlan)
                .Where(um => um.UserId == userId && um.TrangThai == "Đang hoạt động" && um.NgayKetThuc >= DateTime.UtcNow)
                .OrderByDescending(um => um.NgayBatDau)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<UserMembership>> GetUserMemberByUserIdAndOptionId(string userId,string optionId, CancellationToken cancellationToken = default)
        {
            return await _membershipContext.UserMemberships.Where(u => u.UserId == userId && u.OptionId == optionId && u.TrangThai == "Đang hoạt động").ToListAsync();
        }

        // Kiểm tra user đang sử dụng gói thành viên nào
        public async Task<bool> HasActiveMembershipAsync(string userId, string optionId, CancellationToken cancellationToken = default)
        {
            return await _membershipContext.UserMemberships.AnyAsync(
                um => um.UserId == userId &&
                      um.OptionId == optionId &&
                      um.TrangThai == "Đang hoạt động" &&
                      um.NgayKetThuc >= DateTime.UtcNow,
                cancellationToken);
        }
    }
}
