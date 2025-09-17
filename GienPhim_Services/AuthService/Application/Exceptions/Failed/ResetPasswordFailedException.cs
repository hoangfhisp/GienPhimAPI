using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class ResetPasswordFailedException : BusinessException
    {
        public override int StatusCode { get; }
        public ResetPasswordFailedException(string msg, int statusCode = 500)
            : base(msg) 
        {
            StatusCode = statusCode;
        }
    }
}
