using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators
{
    public interface IChatGroupOrchestrator
    {
        Task<ChatGroup> CreateChatGroupAsync(ChatGroupCreateDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        Task UpdateSettingsAsync(ChatGroup group, ChatGroupUpdateSettingsDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        Task RefreshInviteCodeAsync(ChatGroup group, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        Task DisableInviteCodeAsync(ChatGroup group, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);

        Task CloseChatGroupAsync(ChatGroup group, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default);
    }
}
