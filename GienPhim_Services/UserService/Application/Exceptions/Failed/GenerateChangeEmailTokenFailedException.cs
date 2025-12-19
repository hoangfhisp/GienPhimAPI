using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Failed
{
    public class GenerateChangeEmailTokenFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode; 

        public GenerateChangeEmailTokenFailedException(string email, string newEmail)
            : base($"Tạo mã token để đổi email $'{email}' thành '{newEmail}' thất bại.")
        {
        }
    }
}
