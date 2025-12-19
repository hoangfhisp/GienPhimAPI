using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Infrastructure.Repositories
{
    public class GroupMemberRepository : IGroupMemberRepository
    {
        private readonly InteractionDbContext _context;

        public GroupMemberRepository(InteractionDbContext context)
        {
            _context = context;
        }

        public async Task<GroupMember?> GetByIdAsync(Guid groupMemberId, CancellationToken cancellationToken = default)
        {
            return await _context.GroupMembers
                .FirstOrDefaultAsync(x => x.GroupMemberId == groupMemberId, cancellationToken);
        }

        public async Task<GroupMember?> GetByGroupAndUserAsync(Guid groupId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.GroupMembers
                .FirstOrDefaultAsync(x => x.GroupId == groupId && x.UserId == userId, cancellationToken);
        }

        public async Task<IReadOnlyList<GroupMember>> GetByGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
        {
            return await _context.GroupMembers
                .Where(x => x.GroupId == groupId)
                .OrderByDescending(x => x.IsOwner)       // chủ phòng lên trên
                .ThenBy(x => x.JoinedAt)                 // còn lại sort theo thời gian join
                .ToListAsync(cancellationToken);
        }

        public async Task<(IReadOnlyList<GroupMember> Items, int Total, int LastPage)> GetByGroupPagedAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.GroupMembers
                .Where(x => x.GroupId == groupId);

            var total = await query.CountAsync(cancellationToken);
            var lastPage = (int)Math.Ceiling(total / (double)pageSize);

            var items = await query
                .OrderByDescending(x => x.IsOwner)
                .ThenBy(x => x.JoinedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, total, lastPage);
        }

        public async Task<bool> IsLastOwnerAsync(Guid groupId, string userId, CancellationToken cancellationToken = default)
        {
            // Đếm số owner trong group
            var ownersCount = await _context.GroupMembers
                .CountAsync(x => x.GroupId == groupId && x.IsOwner, cancellationToken);

            if (ownersCount <= 1)
            {
                // Nếu có <=1 owner thì user đó chính là owner cuối?
                var isOwner = await _context.GroupMembers
                    .AnyAsync(x => x.GroupId == groupId && x.UserId == userId && x.IsOwner, cancellationToken);

                return isOwner && ownersCount == 1;
            }

            return false;
        }

        public async Task AddAsync(GroupMember member, CancellationToken cancellationToken = default)
        {
            await _context.GroupMembers.AddAsync(member, cancellationToken);
        }

        public Task UpdateAsync(GroupMember member, CancellationToken cancellationToken = default)
        {
            _context.GroupMembers.Update(member);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(GroupMember member, CancellationToken cancellationToken = default)
        {
            _context.GroupMembers.Remove(member);
            return Task.CompletedTask;
        }
    }
}