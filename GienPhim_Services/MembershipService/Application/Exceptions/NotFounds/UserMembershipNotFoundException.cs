using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.NotFounds
{
    public class UserMembershipNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public UserMembershipNotFoundException(string? userMembershipId = "")
            : base($"UserMembership {userMembershipId} không tồn tại.") { }
    }
}
