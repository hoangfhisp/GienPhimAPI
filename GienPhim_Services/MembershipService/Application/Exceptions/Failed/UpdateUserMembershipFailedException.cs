using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class UpdateUserMembershipFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateUserMembershipFailedException(string userMembershipId)
            : base($"Cập nhật UserMembership với mã {userMembershipId} thất bại.") { }
    }
}
