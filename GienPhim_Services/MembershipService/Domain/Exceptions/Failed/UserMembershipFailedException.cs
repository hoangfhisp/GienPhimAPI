using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Failed
{
    public class UserMembershipFailedException : DomainException
    {
        public UserMembershipFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với UserMembership thất bại: {reason}.") { }
    }
}
