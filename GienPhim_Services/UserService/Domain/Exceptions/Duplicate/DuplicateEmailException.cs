using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Duplicate
{
    public class DuplicateEmailException : DomainException
    {
        public DuplicateEmailException(string email) : base($"Email '{email}' đã tồn tại trong hệ thống!")
        {
        }
    }
}
