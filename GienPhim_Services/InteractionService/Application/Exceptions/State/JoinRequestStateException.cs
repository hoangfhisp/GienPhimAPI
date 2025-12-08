using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Application.Exceptions.State
{
    public class JoinRequestStateException : BusinessException
    {
        public override int StatusCode => 409;

        public JoinRequestStateException()
            : base("Trạng thái yêu cầu tham gia không cho phép thao tác này.") { }

        public JoinRequestStateException(string message)
            : base(message) { }
    }
}
