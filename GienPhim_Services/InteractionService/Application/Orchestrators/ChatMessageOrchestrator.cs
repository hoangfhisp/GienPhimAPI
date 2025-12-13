using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Orchestrators
{
    public class ChatMessageOrchestrator : IChatMessageOrchestrator
    {
        private readonly IChatMessageDomainService _chatMessageDomainService;
        private readonly IChatMessageFactory _chatMessageFactory;
        private readonly ILogger<ChatMessageOrchestrator> _logger;

        public ChatMessageOrchestrator(
            IChatMessageDomainService chatMessageDomainService,
            IChatMessageFactory chatMessageFactory,
            ILogger<ChatMessageOrchestrator> logger)
        {
            _chatMessageDomainService = chatMessageDomainService;
            _chatMessageFactory = chatMessageFactory;
            _logger = logger;
        }

        public Task<ChatMessage> CreateUserMessageAsync(ChatGroup group, GroupMember? member, ChatMessageCreateDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu tạo ChatMessage user cho GroupId={GroupId}, UserId={UserId}", dto.GroupId, currentUserId);

            // 1. Domain rules: quyền gửi, trạng thái phòng, content...
            _chatMessageDomainService.EnsureCanSend(group, member, dto.Content, isSystemMessage: false, nowUtc);

            // 2. Factory: dựng entity
            var message = _chatMessageFactory.CreateUserMessage(dto, currentUserId, nowUtc);

            _logger.LogInformation("Orchestrator: Tin nhắn user cho GroupId={GroupId}, UserId={UserId} đã được khởi tạo & validate.", dto.GroupId, currentUserId);

            return Task.FromResult(message);
        }

        public Task<ChatMessage> CreateSystemMessageAsync(ChatGroup group, string content, DateTime nowUtc, string? loaiTinNhan = "system", CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu tạo ChatMessage hệ thống cho GroupId={GroupId}", group.GroupId);

            // 1. Domain rules: vẫn có thể reuse EnsureCanSend, nhưng isSystemMessage = true
            _chatMessageDomainService.EnsureCanSend(group, member: null, content: content, isSystemMessage: true, nowUtc: nowUtc);

            // 2. Factory: dựng system message
            var message = _chatMessageFactory.CreateSystemMessage(group.GroupId, content, nowUtc, loaiTinNhan);

            _logger.LogInformation("Orchestrator: Tin nhắn hệ thống cho GroupId={GroupId} đã được khởi tạo.", group.GroupId);

            return Task.FromResult(message);
        }
    }
}
