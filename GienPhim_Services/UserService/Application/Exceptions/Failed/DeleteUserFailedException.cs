using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class DeleteUserFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public DeleteUserFailedException(string? userName) : base($"Xóa người dùng '{userName}' thất bại!")
        {
        }
    }
}
