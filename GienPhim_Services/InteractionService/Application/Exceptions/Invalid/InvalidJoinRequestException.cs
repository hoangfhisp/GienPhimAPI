using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Invalid
{
    public class InvalidJoinRequestException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidJoinRequestException()
            : base("Yêu cầu tham gia phòng chat không hợp lệ.") { }

        public InvalidJoinRequestException(string message)
            : base(message) { }
    }
}
