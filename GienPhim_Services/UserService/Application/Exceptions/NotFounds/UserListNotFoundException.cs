using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.NotFounds
{
    public class UserListNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public UserListNotFoundException()
            : base("Không tìm thấy danh sách người dùng nào.") { }
    }
}
