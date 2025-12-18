using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.Invalids
{
    public class InvalidAmountException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public InvalidAmountException()
            : base("Số tiền không đủ để giao dịch, không hợp lệ.")
        {
        }
    }
}
