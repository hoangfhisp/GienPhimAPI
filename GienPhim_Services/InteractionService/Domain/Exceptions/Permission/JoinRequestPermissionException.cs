using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Permission
{
    public class JoinRequestPermissionException : DomainException
    {
        public JoinRequestPermissionException(string message) : base(message) { }
    }
}
