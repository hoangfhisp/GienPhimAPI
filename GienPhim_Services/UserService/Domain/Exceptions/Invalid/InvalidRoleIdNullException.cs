using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidRoleIdNullException : DomainException
    {
        public InvalidRoleIdNullException() : base("Role Id không được để trống!")
        {
        }
    }
}
