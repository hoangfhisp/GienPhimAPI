using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Exceptions.Invalid;
using GienPhim_Services.InteractionService.Domain.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Domain.Exceptions.Permission;
using GienPhim_Services.InteractionService.Domain.Exceptions.Expired;
using GienPhim_Services.InteractionService.Domain.Exceptions.State;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;

namespace GienPhim_Services.InteractionService.Domain.Services
{
    public class ChatGroupDomainService : IChatGroupDomainService
    {
        // Tạo phòng chat
        public void EnsureCanCreate(ChatGroup group)
        {
            if (group == null)
                throw new InvalidChatGroupException("Dữ liệu phòng chat không hợp lệ.");

            ValidatePhim(group.PhimId, group.PhimName);

            if (string.IsNullOrWhiteSpace(group.CreatedByUserId))
                throw new InvalidChatGroupException("Thiếu thông tin người tạo phòng.");
        }

        // Cập nhật cài đặt phòng chat
        public void EnsureCanUpdateSettings(ChatGroup group, string currentUserId, DateTime nowUtc)
        {
            EnsureGroupExists(group);
            EnsureOwner(group, currentUserId);
            EnsureNotExpired(group, nowUtc);
            EnsureNotClosed(group);
        }

        // Làm mới mã mời
        public void EnsureCanRefreshInviteCode(ChatGroup group, string currentUserId, DateTime nowUtc)
        {
            EnsureGroupExists(group);
            EnsureOwner(group, currentUserId);
            EnsureNotExpired(group, nowUtc);
            EnsureNotClosed(group);
        }

        // Tắt mã mời
        public void EnsureCanDisableInviteCode(ChatGroup group, string currentUserId, DateTime nowUtc)
        {
            EnsureGroupExists(group);
            EnsureOwner(group, currentUserId);
            EnsureNotExpired(group, nowUtc);
            EnsureNotClosed(group);
        }

        // Đóng phòng chat
        public void EnsureCanClose(ChatGroup group, string currentUserId)
        {
            EnsureGroupExists(group);
            EnsureOwner(group, currentUserId);
            EnsureNotClosed(group);
        }

        // Hàm kiểm tra phim khi tạo phòng chat
        private void ValidatePhim(int phimId, string? phimName)
        {
            if (phimId <= 0)
                throw new InvalidChatGroupException("PhimId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(phimName))
                throw new InvalidChatGroupException("Tên phim không hợp lệ.");
        }

        private void EnsureGroupExists(ChatGroup? group)
        {
            if (group is null)
                throw new ChatGroupNotFoundException("Không tìm thấy phòng chat.");
        }

        private void EnsureOwner(ChatGroup group, string currentUserId)
        {
            if (!string.Equals(group.CreatedByUserId, currentUserId, StringComparison.OrdinalIgnoreCase))
                throw new NotChatGroupOwnerException("Chỉ chủ phòng mới được thực hiện thao tác này.");
        }

        private void EnsureNotExpired(ChatGroup group, DateTime nowUtc)
        {
            if (group.AutoDeleteAt <= nowUtc)
                throw new ChatGroupExpiredException("Phòng chat đã hết hạn.");
        }

        private void EnsureNotClosed(ChatGroup group)
        {
            if (group.IsDeleted)
                throw new ChatGroupAlreadyClosedException("Phòng chat đã được đóng trước đó.");
        }
    }
}
