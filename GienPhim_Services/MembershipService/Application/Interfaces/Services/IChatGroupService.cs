using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Services
{
    public interface IChatGroupService
    {
        Task<ResultDTO<ChatGroupResponseDTO>> GetDetailAsync(Guid groupId, CancellationToken cancellationToken = default);

        Task<ResultListDTO<List<ChatGroupListItemDTO>>> GetByFilmAsync(
            int phimId,
            string? status,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<ResultListDTO<List<ChatGroupListItemDTO>>> GetMineAsync(
            string? role,
            string? status,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<ResultDTO<ChatGroupResponseDTO>> CreateChatGroupAsync(
            ChatGroupCreateDTO dto,
            CancellationToken cancellationToken = default);

        Task<ResultDTO<ChatGroupResponseDTO>> UpdateSettingsAsync(
            Guid groupId,
            ChatGroupUpdateSettingsDTO dto,
            CancellationToken cancellationToken = default);

        Task<ResultDTO<ChatGroupResponseDTO>> RefreshInviteCodeAsync(
            Guid groupId,
            CancellationToken cancellationToken = default);

        Task<ResultDTO<ChatGroupResponseDTO>> DisableInviteCodeAsync(
            Guid groupId,
            CancellationToken cancellationToken = default);

        Task<ResultDTO<ChatGroupResponseDTO>> CloseChatGroupAsync(
            Guid groupId,
            CancellationToken cancellationToken = default);
    }
}
