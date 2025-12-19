using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Infrastructure.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly InteractionDbContext _context;

        public ChatMessageRepository(InteractionDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage?> GetByIdAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            return await _context.ChatMessages
                .FirstOrDefaultAsync(x => x.MessageId == messageId, cancellationToken);
        }

        public async Task<(IReadOnlyList<ChatMessage> Items, int Total, int LastPage)> GetByGroupAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 20; // default: 20 message / page

            var query = _context.ChatMessages.Where(x => x.GroupId == groupId);

            var total = await query.CountAsync(cancellationToken);
            var lastPage = (int)Math.Ceiling(total / (double)pageSize);

            // Lấy tin nhắn mới nhất trước (SentAt desc)
            var items = await query
                .OrderByDescending(x => x.SentAt)
                .ThenByDescending(x => x.MessageId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, total, lastPage);
        }

        public async Task AddAsync(ChatMessage message, CancellationToken cancellationToken = default)
        {
            await _context.ChatMessages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
