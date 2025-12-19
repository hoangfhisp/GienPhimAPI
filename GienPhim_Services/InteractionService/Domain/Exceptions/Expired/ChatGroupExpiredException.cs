using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.State
{
    public class ChatGroupExpiredException : DomainException
    {
        public ChatGroupExpiredException()
            : base("Phòng chat đã hết hạn, không thể thực hiện thao tác này.") { }

        public ChatGroupExpiredException(string reason)
            : base($"Phòng chat đã hết hạn: {reason}") { }
    }
}