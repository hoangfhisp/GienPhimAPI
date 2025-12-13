using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.NotFounds
{
    public class ChatGroupNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public ChatGroupNotFoundException()
            : base("Không tìm thấy phòng chat.") { }

        public ChatGroupNotFoundException(string message)
            : base(message) { }
    }
}
