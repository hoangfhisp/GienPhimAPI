using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Orchestrators
{
    public class ChatGroupOrchestrator : IChatGroupOrchestrator
    {
        private readonly IChatGroupFactory _chatGroupFactory;
        private readonly IChatGroupDomainService _chatGroupDomainService;
        private readonly ILogger<ChatGroupOrchestrator> _logger;

        public ChatGroupOrchestrator(
            IChatGroupFactory chatGroupFactory,
            IChatGroupDomainService chatGroupDomainService,
            ILogger<ChatGroupOrchestrator> logger)
        {
            _chatGroupFactory = chatGroupFactory;
            _chatGroupDomainService = chatGroupDomainService;
            _logger = logger;
        }

        // Tạo phòng chat mới
        public Task<ChatGroup> CreateChatGroupAsync(ChatGroupCreateDTO dto,string currentUserId,DateTime nowUtc,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu tạo phòng chat cho phim {PhimId} - {PhimName}", dto.PhimId, dto.PhimName);

            // 1. Factory: dựng entity từ DTO + userId + time
            var group = _chatGroupFactory.CreateChatGroup(dto, currentUserId, nowUtc);

            // 2. DomainService: validate rule nghiệp vụ
            _chatGroupDomainService.EnsureCanCreate(group);

            _logger.LogInformation("Orchestrator: Phòng chat cho phim {PhimId} đã được khởi tạo & validate thành công.", dto.PhimId);

            return Task.FromResult(group);
        }

        // Cập nhật 3 flag AllowChat / AllowInvite / AllowJoinByLink
        public Task UpdateSettingsAsync(ChatGroup group,ChatGroupUpdateSettingsDTO dto,string currentUserId,DateTime nowUtc,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Cập nhật cài đặt phòng chat {GroupId}", group.GroupId);

            // 1. Check quyền & trạng thái group
            _chatGroupDomainService.EnsureCanUpdateSettings(group, currentUserId, nowUtc);

            // 2. Apply thay đổi
            group.AllowChat = dto.AllowChat;
            group.AllowInvite = dto.AllowInvite;
            group.AllowJoinByLink = dto.AllowJoinByLink;

            _logger.LogInformation("Orchestrator: Đã cập nhật cài đặt phòng chat {GroupId}: AllowChat={AllowChat}, AllowInvite={AllowInvite}, AllowJoinByLink={AllowJoinByLink}",
                group.GroupId, group.AllowChat, group.AllowInvite, group.AllowJoinByLink);

            return Task.CompletedTask;
        }

        // Làm mới mã mời
        public Task RefreshInviteCodeAsync(ChatGroup group,string currentUserId,DateTime nowUtc,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Làm mới mã mời cho phòng chat {GroupId}", group.GroupId);

            _chatGroupDomainService.EnsureCanRefreshInviteCode(group, currentUserId, nowUtc);

            group.InviteCode = GenerateInviteCode();
            group.InviteCodeExpireAt = group.AutoDeleteAt;

            _logger.LogInformation("Orchestrator: Mã mời mới cho phòng {GroupId} là {InviteCode}, hết hạn lúc {ExpireAt}",
                group.GroupId, group.InviteCode, group.InviteCodeExpireAt);

            return Task.CompletedTask;
        }

        // Tắt mã mời
        public Task DisableInviteCodeAsync(ChatGroup group,string currentUserId,DateTime nowUtc,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Tắt mã mời cho phòng chat {GroupId}", group.GroupId);

            _chatGroupDomainService.EnsureCanDisableInviteCode(group, currentUserId, nowUtc);

            group.InviteCode = null;
            group.InviteCodeExpireAt = null;

            _logger.LogInformation("Orchestrator: Mã mời cho phòng {GroupId} đã bị vô hiệu hoá", group.GroupId);

            return Task.CompletedTask;
        }

        // Đóng phòng chat
        public Task CloseChatGroupAsync(ChatGroup group,string currentUserId,DateTime nowUtc,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Đóng phòng chat {GroupId}", group.GroupId);

            _chatGroupDomainService.EnsureCanClose(group, currentUserId);

            group.IsDeleted = true;
            group.InviteCode = null;
            group.InviteCodeExpireAt = null;
            group.AutoDeleteAt = nowUtc; // ép auto-delete = now

            _logger.LogInformation("Orchestrator: Phòng chat {GroupId} đã được đóng bởi {UserId}", group.GroupId, currentUserId);

            return Task.CompletedTask;
        }

        // Private helper
        private string GenerateInviteCode()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        }
    }
}
