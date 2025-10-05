using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class UpdatePlanOptionFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdatePlanOptionFailedException(string optionId)
            : base($"Cập nhật PlanOption với mã {optionId} thất bại.") { }
    }
}
