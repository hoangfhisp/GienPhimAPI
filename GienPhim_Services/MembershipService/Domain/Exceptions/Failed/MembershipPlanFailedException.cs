using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Failed
{
    public class MembershipPlanFailedException : DomainException
    {
        public MembershipPlanFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với MembershipPlan thất bại: {reason}.") { }
    }
}
