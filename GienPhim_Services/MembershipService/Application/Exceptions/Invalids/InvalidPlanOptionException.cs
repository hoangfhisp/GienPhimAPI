using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Invalids
{
    public class InvalidPlanOptionException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidPlanOptionException(string reason)
            : base($"Dữ liệu PlanOption không hợp lệ: {reason}") { }
    }
}
