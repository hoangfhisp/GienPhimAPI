using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.State
{
    public class JoinRequestStateException : DomainException
    {
        public JoinRequestStateException(string message) : base(message) { }
    }
}
