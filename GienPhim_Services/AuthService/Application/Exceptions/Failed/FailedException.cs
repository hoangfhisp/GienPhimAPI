using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class FailedException : BusinessException
    {
        public override int StatusCode { get; } // BadRequest
        public FailedException(string message, int statusCode = 400)
            : base($"Thao tác thất bại: {message}")
        {
            StatusCode = statusCode;
        }
    }
}
