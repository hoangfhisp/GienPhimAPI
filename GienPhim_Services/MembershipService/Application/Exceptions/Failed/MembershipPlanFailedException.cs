using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Failed
{
    public class MembershipPlanFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public MembershipPlanFailedException(string action, string reason)
            : base($"Không thể {action} MembershipPlan: {reason}") { }
    }
}
