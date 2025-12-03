using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.NotFounds
{
    public class GroupMemberNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public GroupMemberNotFoundException()
            : base("Không tìm thấy thành viên phòng chat.") { }

        public GroupMemberNotFoundException(string message)
            : base(message) { }
    }
}
