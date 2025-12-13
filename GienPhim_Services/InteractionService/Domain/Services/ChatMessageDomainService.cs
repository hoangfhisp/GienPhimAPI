using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Exceptions.Invalid;
using GienPhim_Services.InteractionService.Domain.Exceptions.Permission;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;

namespace GienPhim_Services.InteractionService.Domain.Services
{
    public class ChatMessageDomainService : IChatMessageDomainService
    {
        public void EnsureCanSend(ChatGroup group, GroupMember? member, string content, bool isSystemMessage, DateTime nowUtc)
        {
            if (group is null) throw new InvalidChatMessageException("Không thể gửi tin nhắn vào phòng chat không tồn tại.");

            // Phòng đã đóng / hết hạn
            if (group.IsDeleted || nowUtc > group.AutoDeleteAt || nowUtc > group.ShowEndTime) throw new InvalidChatMessageException("Phòng chat đã kết thúc hoặc bị đóng, không thể gửi tin nhắn.");

            // Phòng đang tắt chat: chỉ cho phép tin nhắn hệ thống
            if (!group.AllowChat && !isSystemMessage) throw new InvalidChatMessageException("Phòng chat đang tạm khoá, không thể gửi tin nhắn.");

            // Validate nội dung
            var v = (content ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v)) throw new InvalidChatMessageException("Nội dung tin nhắn không được để trống.");
            if (v.Length > 500) throw new InvalidChatMessageException("Nội dung tin nhắn không được vượt quá 500 ký tự.");

            // Tin nhắn user gửi (không phải system) thì phải là member hợp lệ
            if (!isSystemMessage)
            {
                if (member is null) throw new ChatMessagePermissionException("Bạn không phải thành viên của phòng chat, không thể gửi tin nhắn.");

                if (!member.CanChat || member.IsMuted) throw new ChatMessagePermissionException("Bạn không được phép gửi tin nhắn trong phòng chat này.");
            }

        }
    }
}
