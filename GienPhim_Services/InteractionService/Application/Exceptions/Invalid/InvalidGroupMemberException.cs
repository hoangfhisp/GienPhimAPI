using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.Invalid
{
    public class InvalidGroupMemberException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidGroupMemberException()
            : base("Dữ liệu thành viên không hợp lệ.") { }

        public InvalidGroupMemberException(string message)
            : base(message) { }
    }
}
