using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.NotFounds
{
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException(string? userName = "")
            : base($"Không tìm thấy người dùng với tài khoản: {userName}") { }
    }
}
