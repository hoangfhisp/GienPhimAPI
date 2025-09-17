using GienPhim_Services.UserService.Shared.Exceptions;

namespace GienPhim_Services.UserService.Application.Exceptions.Duplicates
{
    public class DuplicateAuthIdException : BusinessException
    {
        public override int StatusCode => 409;
        public DuplicateAuthIdException(string authId)
            : base($"AuthId '{authId}' của người dùng này đã tồn tại.") { }
    }
}
