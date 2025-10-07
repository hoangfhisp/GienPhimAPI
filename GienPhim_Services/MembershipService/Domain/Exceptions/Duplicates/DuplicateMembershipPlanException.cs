using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Duplicates
{
    public class DuplicateMembershipPlanException : DomainException
    {
        public DuplicateMembershipPlanException(string tenPlan)
            : base($"MembershipPlan '{tenPlan}' đã tồn tại.") { }
    }
}