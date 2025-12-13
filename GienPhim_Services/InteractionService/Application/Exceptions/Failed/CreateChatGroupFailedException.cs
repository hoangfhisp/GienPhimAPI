using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Failed
{
    public class CreateChatGroupFailedException : BusinessException
    {
        public override int StatusCode => 500;

        public CreateChatGroupFailedException()
            : base("Tạo phòng chat thất bại.") { }

        public CreateChatGroupFailedException(int phimId)
            : base($"Tạo phòng chat cho phim Id={phimId} thất bại.") { }

        public CreateChatGroupFailedException(string message)
            : base(message) { }
    }
}
