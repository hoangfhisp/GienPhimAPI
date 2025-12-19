using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Expired
{
    public class ChatGroupExpiredException : BusinessException
    {
        // 410 Gone: tài nguyên từng tồn tại nhưng giờ đã hết hạn / không còn
        public override int StatusCode => 410;

        public ChatGroupExpiredException()
            : base("Phòng chat đã hết hạn.") { }

        public ChatGroupExpiredException(string message)
            : base(message) { }
    }
}
