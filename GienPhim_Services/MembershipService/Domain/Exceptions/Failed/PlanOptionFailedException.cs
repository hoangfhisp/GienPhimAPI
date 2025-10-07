using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Failed
{
    public class PlanOptionFailedException : DomainException
    {
        public PlanOptionFailedException(string action, string reason)
            : base($"Thực hiện '{action}' với PlanOption thất bại: {reason}.") { }
    }
}
