using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Repositories
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage?> GetByIdAsync(Guid messageId, CancellationToken cancellationToken = default);

        // Lấy danh sách tin nhắn trong 1 group (có phân trang).
        Task<(IReadOnlyList<ChatMessage> Items, int Total, int LastPage)> GetByGroupAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default);

        // Thêm tin nhắn mới.
        Task AddAsync(ChatMessage message, CancellationToken cancellationToken = default);
    }
}
