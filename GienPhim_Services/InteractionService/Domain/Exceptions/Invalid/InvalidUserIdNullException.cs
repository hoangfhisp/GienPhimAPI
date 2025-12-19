using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidUserIdNullException : DomainException
    {
        public InvalidUserIdNullException()
            : base("Dữ liệu UserId không được để trống.")
        {
        }
    }
}
