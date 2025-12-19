using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Duplicates
{
    public class DuplicateUserMembershipException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateUserMembershipException(string userId, string optionId)
            : base($"Người dùng '{userId}' đã đăng ký gói lựa chọn '{optionId}' hiện đang còn hiệu lực.") { }
    }
}
