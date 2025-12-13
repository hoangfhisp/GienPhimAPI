using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Duplicate
{
    public class DuplicateChatGroupException : DomainException
    {
        public DuplicateChatGroupException(string reason)
            : base($"Phòng chat bị trùng: {reason}.") { }
    }

}
