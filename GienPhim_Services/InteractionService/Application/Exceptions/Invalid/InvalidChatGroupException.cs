using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Invalid
{
    public class InvalidChatGroupException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidChatGroupException()
            : base("Dữ liệu phòng chat không hợp lệ.") { }

        public InvalidChatGroupException(string message)
            : base(message) { }
    }
}
