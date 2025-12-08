using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidJoinRequestException : DomainException
    {
        public InvalidJoinRequestException(string message) : base(message) { }
    }
}
