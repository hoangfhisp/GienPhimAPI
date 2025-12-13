using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Duplicate
{
    public class DuplicateJoinRequestException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateJoinRequestException(string message)
            : base(message) { }

        public DuplicateJoinRequestException(string userId, Guid groupId)
            : base($"Người dùng {userId} đã có yêu cầu tham gia phòng chat {groupId} đang chờ xử lý.") { }
    }
}
