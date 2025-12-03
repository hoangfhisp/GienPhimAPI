using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.State
{
    public class ChatGroupAlreadyClosedException : BusinessException
    {
        public override int StatusCode => 409;

        public ChatGroupAlreadyClosedException()
            : base("Phòng chat đã được đóng trước đó.") { }

        public ChatGroupAlreadyClosedException(string message)
            : base(message) { }
    }
}
