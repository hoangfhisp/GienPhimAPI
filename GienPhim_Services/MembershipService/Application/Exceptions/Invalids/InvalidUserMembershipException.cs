using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Invalids
{
    public class InvalidUserMembershipException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidUserMembershipException(string reason)
            : base($"Dữ liệu UserMembership không hợp lệ: {reason}") { }
    }
}
