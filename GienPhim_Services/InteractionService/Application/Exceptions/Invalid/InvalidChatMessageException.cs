using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Invalid
{
    public class InvalidChatMessageException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidChatMessageException()
            : base("Tin nhắn không hợp lệ.") { }

        public InvalidChatMessageException(string message)
            : base(message) { }
    }
}
