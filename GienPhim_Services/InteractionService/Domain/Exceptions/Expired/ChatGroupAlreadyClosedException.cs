using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Expired
{
    public class ChatGroupAlreadyClosedException : DomainException
    {
        public ChatGroupAlreadyClosedException()
            : base("Phòng chat đã được đóng trước đó.") { }

        public ChatGroupAlreadyClosedException(string reason)
            : base($"Phòng chat đã được đóng: {reason}") { }
    }
}
