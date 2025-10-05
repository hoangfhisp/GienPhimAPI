using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.Duplicates
{
    public class DuplicatePlanOptionException : DomainException
    {
        public DuplicatePlanOptionException(string optionId)
            : base($"PlanOption với Id '{optionId}' đã tồn tại.") { }

        public DuplicatePlanOptionException(string planId, int thoiGianSuDung)
            : base($"PlanOption với PlanId '{planId}' và Thời gian sử dụng {thoiGianSuDung} ngày đã tồn tại.") { }
    }
}
