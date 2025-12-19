using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Application.Exceptions.Failed
{
    public class SaveRefreshTokenFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;
        public SaveRefreshTokenFailedException() : base("Lưu refresh token thất bại")
        {
        }
    }
}
