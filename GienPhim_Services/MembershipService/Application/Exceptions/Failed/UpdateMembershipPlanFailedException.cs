using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class UpdateMembershipPlanFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateMembershipPlanFailedException(string planName)
            : base($"Cập nhật MembershipPlan với tên {planName} thất bại.") { }
    }
}
