using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Failed
{
    public class GenerateResetTokenFailedException : DomainException
    {
        public GenerateResetTokenFailedException(string? account)
            : base($"Tạo mã token để đặt lại mật khẩu cho {account} không thành công.") { }
    }
}
