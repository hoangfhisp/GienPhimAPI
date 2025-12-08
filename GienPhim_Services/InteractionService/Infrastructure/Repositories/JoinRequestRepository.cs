using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Infrastructure.Repositories
{
    public class JoinRequestRepository : IJoinRequestRepository
    {
        private readonly InteractionDbContext _context;

        public JoinRequestRepository(InteractionDbContext context)
        {
            _context = context;
        }

        public async Task<JoinRequest?> GetByIdAsync(Guid requestId, CancellationToken cancellationToken = default)
        {
            return await _context.JoinRequests
                .FirstOrDefaultAsync(x => x.RequestId == requestId, cancellationToken);
        }

        public async Task<JoinRequest?> GetPendingByGroupAndUserAsync(Guid groupId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.JoinRequests
                .FirstOrDefaultAsync(x =>
                    x.GroupId == groupId
                    && x.RequestUserId == userId
                    && x.Status == "Pending",
                    cancellationToken);
        }

        public async Task<(IReadOnlyList<JoinRequest> Items, int Total, int LastPage)> GetByGroupAsync(Guid groupId, string? status, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.JoinRequests
                .Where(x => x.GroupId == groupId);

            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim();
                query = query.Where(x => x.Status == status);
            }

            var total = await query.CountAsync(cancellationToken);
            var lastPage = (int)Math.Ceiling(total / (double)pageSize);

            var items = await query
                .OrderBy(x => x.Status)          // Pending trước, rồi Accepted/Rejected
                .ThenByDescending(x => x.RequestedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, total, lastPage);
        }

        public async Task AddAsync(JoinRequest request, CancellationToken cancellationToken = default)
        {
            await _context.JoinRequests.AddAsync(request, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(JoinRequest request, CancellationToken cancellationToken = default)
        {
            _context.JoinRequests.Update(request);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
