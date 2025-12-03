using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Services
{
    public interface IGroupMemberService
    {
        Task<ResultListDTO<List<GroupMemberResponseDTO>>> GetMembersByGroupAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default);

        Task<ResultDTO<GroupMemberResponseDTO>> JoinGroupAsync(GroupMemberJoinDTO dto, CancellationToken cancellationToken = default);

        Task<ResultDTO<GroupMemberResponseDTO>> LeaveGroupAsync(Guid groupId, CancellationToken cancellationToken = default);

        Task<ResultDTO<GroupMemberResponseDTO>> UpdatePermissionsAsync(Guid groupId, string targetUserId, GroupMemberUpdatePermissionsDTO dto, CancellationToken cancellationToken = default);

        Task<ResultDTO<GroupMemberResponseDTO>> TransferOwnershipAsync(Guid groupId, string newOwnerUserId, CancellationToken cancellationToken = default);
    }
}
