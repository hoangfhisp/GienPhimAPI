using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class PlanOptionFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public PlanOptionFailedException(string action, string reason)
            : base($"Không thể {action} PlanOption: {reason}") { }
    }
}
