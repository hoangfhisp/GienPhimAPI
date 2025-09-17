using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Failed
{
    public class ResetPasswordFailedException : DomainException
    {
        public ResetPasswordFailedException(string? account = "")
            : base($"Đặt lại mật khẩu cho {account} không thành công, vui lòng thử lại sau.") { }
    }
}
