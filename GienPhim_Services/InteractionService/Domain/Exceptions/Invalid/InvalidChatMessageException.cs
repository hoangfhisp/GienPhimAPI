using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidChatMessageException : DomainException
    {
        public InvalidChatMessageException(string message) : base(message)
        {
        }
    }
}
