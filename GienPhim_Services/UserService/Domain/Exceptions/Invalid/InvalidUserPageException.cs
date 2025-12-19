using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserPageException : DomainException
    {
        public InvalidUserPageException(int page)
            : base($"Số page không hợp lệ: {page}!")
        {
        }
    }
}
