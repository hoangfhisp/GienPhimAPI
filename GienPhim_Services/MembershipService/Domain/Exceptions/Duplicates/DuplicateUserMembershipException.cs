using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Duplicates
{
    public class DuplicateUserMembershipException : DomainException
    {
        public DuplicateUserMembershipException(string userId)
            : base($"Người dùng '{userId}' đã có gói thành viên đang hoạt động.") { }

        public DuplicateUserMembershipException(string userId, string optionId)
            : base($"Người dùng '{userId}' đã có gói thành viên với OptionId = '{optionId}'.") { }
    }
}
