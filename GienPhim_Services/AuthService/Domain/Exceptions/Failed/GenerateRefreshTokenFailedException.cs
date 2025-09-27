using GienPhim_Services.AuthService.shared.Exceptions;
using GienPhim_Services.AuthService.Shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Failed
{
    public class GenerateRefreshTokenFailedException : DomainException
    {
        public GenerateRefreshTokenFailedException() : base("Tạo mới refresh token thất bại")
        {
        }
    }
}
