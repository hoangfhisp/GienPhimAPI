using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidChatGroupException : DomainException
    {
        public InvalidChatGroupException(string reason)
            : base($"Phòng chat không hợp lệ: {reason}.") { }
    }
}
