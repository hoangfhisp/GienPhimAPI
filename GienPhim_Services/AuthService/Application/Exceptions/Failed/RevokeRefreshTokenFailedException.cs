using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class RevokeRefreshTokenFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode; 

        public RevokeRefreshTokenFailedException() : base ("Thu hồi Refresh Token thất bại!")
        {
        }
    }
}
