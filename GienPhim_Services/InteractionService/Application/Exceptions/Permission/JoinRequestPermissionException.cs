using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Permission
{
    public class JoinRequestPermissionException : BusinessException
    {
        public override int StatusCode => 403;

        public JoinRequestPermissionException()
            : base("Bạn không có quyền xử lý yêu cầu tham gia phòng chat này.") { }

        public JoinRequestPermissionException(string message)
            : base(message) { }
    }
}
