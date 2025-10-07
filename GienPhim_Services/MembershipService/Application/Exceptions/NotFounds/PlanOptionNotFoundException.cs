using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.NotFounds
{
    public class PlanOptionNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public PlanOptionNotFoundException(string? optionId = "")
            : base($"PlanOption {optionId} không tồn tại.") { }
    }
}
