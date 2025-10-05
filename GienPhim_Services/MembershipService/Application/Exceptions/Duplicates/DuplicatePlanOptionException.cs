using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Duplicates
{
    public class DuplicatePlanOptionException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicatePlanOptionException(string planId, int thoiGianSuDung)
            : base($"Gói '{planId}' đã có lựa chọn thời gian {thoiGianSuDung} ngày.") { }
    }
}
