using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Duplicate
{
    public class DuplicateJoinRequestException : DomainException
    {
        public DuplicateJoinRequestException(string userId, Guid groupId)
            : base($"Người dùng {userId} đã có yêu cầu tham gia phòng chat {groupId} đang chờ xử lý.") { }
    }
}
