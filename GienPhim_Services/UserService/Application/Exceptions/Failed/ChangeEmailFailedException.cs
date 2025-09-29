using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class ChangeEmailFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public ChangeEmailFailedException(string email, string newEmail) : base($"Đổi email '{email}' sang '{newEmail}' không thành công, vui lòng thử lại!")
        {
        }
    }
}
