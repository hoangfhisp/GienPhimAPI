using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Permission
{
    public class GroupMemberPermissionException : BusinessException
    {
        public override int StatusCode => 403;

        public GroupMemberPermissionException()
            : base("Bạn không có quyền thực hiện thao tác này với thành viên.") { }

        public GroupMemberPermissionException(string message)
            : base(message) { }
    }
}
