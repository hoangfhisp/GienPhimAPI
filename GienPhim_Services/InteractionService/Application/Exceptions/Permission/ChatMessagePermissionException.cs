using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Permission
{
    public class ChatMessagePermissionException : BusinessException
    {
        public override int StatusCode => 403;

        public ChatMessagePermissionException()
            : base("Bạn không có quyền thực hiện thao tác với tin nhắn này.") { }

        public ChatMessagePermissionException(string message)
            : base(message) { }
    }
}
