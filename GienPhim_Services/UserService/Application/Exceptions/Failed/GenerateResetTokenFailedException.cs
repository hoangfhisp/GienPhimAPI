using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class GenerateResetTokenFailedException : BusinessException
    {
        public override int StatusCode => 500;
        public GenerateResetTokenFailedException(string? account)
            : base($"Tạo mã token để đặt lại mật khẩu cho {account} không thành công.") { }
    }
}
