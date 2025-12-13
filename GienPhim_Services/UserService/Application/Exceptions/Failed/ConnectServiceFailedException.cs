using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class ConnectServiceFailedException : BusinessException
    {
        public override int StatusCode => 503; // Service Unavailable 

        public ConnectServiceFailedException(string serviceName)
            : base($"Kết nối đến dịch vụ {serviceName} thất bại. Vui lòng thử lại sau.")
        {
        }
    }
}
