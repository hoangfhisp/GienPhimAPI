using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Invalid
{
    public class InvalidPlanOptionException : DomainException
    {
        public InvalidPlanOptionException(string reason)
            : base($"PlanOption không hợp lệ: {reason}.") { }
    }
}
