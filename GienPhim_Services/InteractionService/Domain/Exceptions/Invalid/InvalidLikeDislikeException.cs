using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidLikeDislikeException : DomainException
    {
        public InvalidLikeDislikeException()
            : base("Phản hồi không thể vừa thích vừa không thích.")
        {
        }
    }
}
