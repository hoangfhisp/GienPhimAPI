using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Invalid
{
    public class InvalidUserMembershipException : DomainException
    {
        public InvalidUserMembershipException(string reason)
            : base($"UserMembership không hợp lệ: {reason}.") { }
    }
}
