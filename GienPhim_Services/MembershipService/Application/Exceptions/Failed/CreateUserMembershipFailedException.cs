using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class CreateUserMembershipFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateUserMembershipFailedException(string userId)
            : base($"Tạo UserMembership cho người dùng {userId} thất bại.") { }
    }
}
