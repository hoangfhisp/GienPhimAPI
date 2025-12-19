using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.NotFounds
{
    public class PhimNotFoundException : BusinessException
    {
        public override int StatusCode { get; } // Not Found

        public PhimNotFoundException(string msg, int statusCode = 404)
            : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}
