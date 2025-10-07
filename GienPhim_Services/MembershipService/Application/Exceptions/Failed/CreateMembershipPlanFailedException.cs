using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class CreateMembershipPlanFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateMembershipPlanFailedException(string planName)
            : base($"Tạo MembershipPlan với tên {planName} thất bại.") { }
    }
}
