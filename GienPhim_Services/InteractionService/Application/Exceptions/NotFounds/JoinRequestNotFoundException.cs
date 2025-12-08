using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.NotFounds
{
    public class JoinRequestNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public JoinRequestNotFoundException()
            : base("Không tìm thấy yêu cầu tham gia phòng chat.") { }

        public JoinRequestNotFoundException(string message)
            : base(message) { }
    }
}
