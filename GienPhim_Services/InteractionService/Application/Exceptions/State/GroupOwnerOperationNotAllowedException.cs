using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.State
{
    public class GroupOwnerOperationNotAllowedException : BusinessException
    {
        public override int StatusCode => 409;

        public GroupOwnerOperationNotAllowedException()
            : base("Thao tác này không hợp lệ đối với chủ phòng.") { }

        public GroupOwnerOperationNotAllowedException(string message)
            : base(message) { }
    }
}
