using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Invalid
{
    public class InvalidAuthProviderException : BusinessException
    {
        public override int StatusCode => 400; // 400 Bad Request
        public InvalidAuthProviderException(string provider)
            : base($"Nhà cung cấp xác thực '{provider}' không hợp lệ hoặc không được hỗ trợ.") { }
    }
}
