using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.State
{
    public class GroupOwnerOperationNotAllowedException : DomainException
    {
        public GroupOwnerOperationNotAllowedException(string message) : base(message) { }
    }
}
