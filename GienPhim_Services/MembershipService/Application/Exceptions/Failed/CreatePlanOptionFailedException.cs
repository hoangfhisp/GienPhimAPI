using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class CreatePlanOptionFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreatePlanOptionFailedException(string optionId)
            : base($"Tạo PlanOption với mã {optionId} thất bại.") { }
    }
}
