using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidGroupMemberException : DomainException
    {
        public InvalidGroupMemberException(string message) : base(message) { }
    }
}
