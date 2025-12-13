using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Duplicate
{
    public class DuplicateGroupMemberException : DomainException
    {
        public DuplicateGroupMemberException(string userId, Guid groupId)
            : base($"Người dùng {userId} đã là thành viên của phòng chat {groupId}.") { }
    }
}
