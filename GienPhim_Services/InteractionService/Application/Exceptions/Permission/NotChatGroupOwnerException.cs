using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Permission
{
    public class NotChatGroupOwnerException : BusinessException
    {
        public override int StatusCode => 403;

        public NotChatGroupOwnerException()
            : base("Chỉ chủ phòng mới được thực hiện thao tác này.") { }

        public NotChatGroupOwnerException(string message)
            : base(message) { }
    }
}
