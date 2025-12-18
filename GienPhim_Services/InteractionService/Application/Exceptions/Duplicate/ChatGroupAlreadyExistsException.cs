using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Duplicate
{
    public class ChatGroupAlreadyExistsException : BusinessException
    {
        // 409 Conflict
        public override int StatusCode => 409;

        public ChatGroupAlreadyExistsException()
            : base("Bạn đã có một phòng xem chung cho phim này đang hoạt động.") { }

        public ChatGroupAlreadyExistsException(int phimId)
            : base($"Bạn đã có một phòng xem chung cho phim Id={phimId} đang hoạt động.") { }

        public ChatGroupAlreadyExistsException(string message)
            : base(message) { }
    }
}
