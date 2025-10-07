using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.NotFounds
{
    public class RoleNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public RoleNotFoundException() : base("Role không tồn tại trong hệ thống!")
        {
        }
    }
}
