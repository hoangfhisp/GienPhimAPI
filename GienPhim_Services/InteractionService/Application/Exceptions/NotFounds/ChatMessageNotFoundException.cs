using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.NotFounds
{
    public class ChatMessageNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public ChatMessageNotFoundException()
            : base("Không tìm thấy tin nhắn.") { }

        public ChatMessageNotFoundException(string message)
            : base(message) { }
    }
}
