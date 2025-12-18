using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidFeedBackNullException : DomainException
    {
        public InvalidFeedBackNullException()
            : base("Dữ liệu phản hồi không được để trống.")
        {
        }
    }
}
