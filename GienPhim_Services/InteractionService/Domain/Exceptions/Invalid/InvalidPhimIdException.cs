using GienPhim_Services.AuthService.shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Invalid
{
    public class InvalidPhimIdException : DomainException
    {
        public InvalidPhimIdException()
            : base("PhimId không hợp lệ.")
        {
        }
    }
}
