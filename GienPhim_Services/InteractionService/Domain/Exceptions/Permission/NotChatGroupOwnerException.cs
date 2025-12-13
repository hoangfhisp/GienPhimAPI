using GienPhim_Services.InteractionService.Shared.Exceptions;

namespace GienPhim_Services.InteractionService.Domain.Exceptions.Permission
{
    public class NotChatGroupOwnerException : DomainException
    {
        public NotChatGroupOwnerException(string? action = null)
            : base(string.IsNullOrWhiteSpace(action)
                ? "Chỉ chủ phòng mới được thực hiện thao tác này."
                : $"Chỉ chủ phòng mới được phép '{action}'.")
        { }
    }
}