using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class HttpResposeFailedException : BusinessException
    {
        public override int StatusCode { get; } // Service Unavailable 
        public HttpResposeFailedException(string msg, int statusCode)
            : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}
