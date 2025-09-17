using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class GenerateTokenUserFailedException : BusinessException
    {
        public override int StatusCode => 500;
        public GenerateTokenUserFailedException(string message)
            : base($"Tạo token thất bại: {message}") { }
    }
}
