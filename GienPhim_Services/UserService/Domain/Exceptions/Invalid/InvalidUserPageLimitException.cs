using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidUserPageLimitException : DomainException
    {
        public InvalidUserPageLimitException(int limit)
            : base($"Giá trị giới hạn trang không hợp lệ: {limit}. Giá trị phải nằm trong khoảng từ 1 đến 100.")
        {
        }
    }
}
