using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Duplicate
{
    public class DuplicateGroupMemberException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateGroupMemberException(string userId, Guid groupId)
            : base($"Người dùng {userId} đã là thành viên của phòng chat {groupId}.") { }

        public DuplicateGroupMemberException(string message)
            : base(message) { }
    }
}
