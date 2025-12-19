using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.NotFounds
{
    public class MembershipPlanNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public MembershipPlanNotFoundException()
            : base("Không tìm thấy MembershipPlan nào") { }

        public MembershipPlanNotFoundException(string planId)
            : base($"Không tìm thấy MembershipPlan với Id = {planId}") { }
    }
}
