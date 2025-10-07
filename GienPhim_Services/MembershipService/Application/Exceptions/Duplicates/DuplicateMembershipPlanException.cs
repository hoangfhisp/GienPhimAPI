using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Duplicates
{
    public class DuplicateMembershipPlanException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateMembershipPlanException(string tenPlan)
            : base($"Gói thành viên '{tenPlan}' đã tồn tại.") { }
    }
}
