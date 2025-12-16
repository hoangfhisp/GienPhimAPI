using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class UserNotFoundException : BusinessException
    {
        public override int StatusCode { get; } // Not Found

        public UserNotFoundException(string msg, int statusCode = 404)
            : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}
