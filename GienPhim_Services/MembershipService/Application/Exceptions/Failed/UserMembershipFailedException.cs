using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class UserMembershipFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UserMembershipFailedException(string action, string reason)
            : base($"Không thể {action} UserMembership: {reason}") { }
    }
}
