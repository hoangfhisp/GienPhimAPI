using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Invalid
{
    public class InvalidMembershipPlanException : DomainException
    {
        public InvalidMembershipPlanException(string reason)
            : base($"MembershipPlan không hợp lệ: {reason}.") { }
    }
}
