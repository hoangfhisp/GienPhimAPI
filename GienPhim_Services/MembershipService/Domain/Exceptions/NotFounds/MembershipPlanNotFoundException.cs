using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds
{
    public class MembershipPlanNotFoundException : DomainException
    {
        public MembershipPlanNotFoundException(string planId)
            : base($"Không tìm thấy MembershipPlan với Id = {planId}.") { }
    }
}
