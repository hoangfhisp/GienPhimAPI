using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Permission
{
    public class ChatMessagePermissionException : DomainException
    {
        public ChatMessagePermissionException(string message) : base(message)
        {
        }
    }
}
