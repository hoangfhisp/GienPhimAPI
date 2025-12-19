using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.NotFounds
{
    public class ChatGroupNotFoundException : DomainException
    {
        public ChatGroupNotFoundException(string groupId)
            : base($"Không tìm thấy phòng chat với Id = {groupId}.") { }

        public ChatGroupNotFoundException()
            : base("Không tìm thấy phòng chat.") { }
    }
}
