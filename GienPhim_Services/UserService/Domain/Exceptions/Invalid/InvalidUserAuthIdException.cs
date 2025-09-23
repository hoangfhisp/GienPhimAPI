using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserAuthIdException : DomainException
    {
        public InvalidUserAuthIdException(string? userAuthId)
            : base($"AuthId người dùng không được để trống: {userAuthId}.")
        {
        }
    }
}
