using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Application.Exceptions.NotFounds
{
    public class UserNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public UserNotFoundException(string? userId = "")
            : base($"Người dùng với Id {userId} không tồn tại.")
        {
        }
    }
}
