using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.AuthService.Domain.Exceptions.Failed
{
    public class GenerateTokenUserFailedException : DomainException
    {
        public GenerateTokenUserFailedException()
            : base($"Tạo token thất bại!") { }
    }
}
