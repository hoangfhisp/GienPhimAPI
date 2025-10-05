using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Invalids
{
    public class InvalidMembershipPlanException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidMembershipPlanException(string reason)
            : base($"Dữ liệu MembershipPlan không hợp lệ: {reason}") { }
    }
}
