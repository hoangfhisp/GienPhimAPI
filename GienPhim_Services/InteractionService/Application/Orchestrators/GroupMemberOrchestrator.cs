using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Orchestrators
{
    public class GroupMemberOrchestrator : IGroupMemberOrchestrator
    {
        private readonly IGroupMemberFactory _groupMemberFactory;
        private readonly IGroupMemberDomainService _groupMemberDomainService;
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly ILogger<GroupMemberOrchestrator> _logger;

        public GroupMemberOrchestrator(
            IGroupMemberFactory groupMemberFactory,
            IGroupMemberDomainService groupMemberDomainService,
            IGroupMemberRepository groupMemberRepository,
            ILogger<GroupMemberOrchestrator> logger)
        {
            _groupMemberFactory = groupMemberFactory;
            _groupMemberDomainService = groupMemberDomainService;
            _groupMemberRepository = groupMemberRepository;
            _logger = logger;
        }

        // User tham gia phòng chat
        public async Task<GroupMember> JoinAsync(ChatGroup group, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu JoinAsync vào phòng chat {GroupId} bởi UserId={UserId}", group.GroupId, currentUserId);

            // 1. Lấy membership hiện tại (nếu đã là member rồi)
            var existing = await _groupMemberRepository.GetByGroupAndUserAsync(group.GroupId, currentUserId, cancellationToken);

            // 2. DomainService: validate rule join
            _groupMemberDomainService.EnsureCanJoin(group, existing, nowUtc);

            // 3. Factory: dựng entity GroupMember mới
            var isOwner = group.CreatedByUserId == currentUserId;
            var member = _groupMemberFactory.CreateForJoin(group, currentUserId, nowUtc, isOwner);

            _logger.LogInformation("Orchestrator: UserId={UserId} đã được khởi tạo GroupMember cho phòng {GroupId} (IsOwner={IsOwner})", currentUserId, group.GroupId, member.IsOwner);

            return member;
        }

        // User rời phòng chat
        public async Task LeaveAsync(ChatGroup group, GroupMember member, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu LeaveAsync phòng chat {GroupId} bởi UserId={UserId}", group.GroupId, member.UserId);

            // 1. Kiểm tra có phải owner cuối cùng không
            var isLastOwner = await _groupMemberRepository.IsLastOwnerAsync(group.GroupId, member.UserId, cancellationToken);

            // 2. DomainService: validate rule leave
            _groupMemberDomainService.EnsureCanLeave(group, member, isLastOwner);

            _logger.LogInformation("Orchestrator: UserId={UserId} có thể rời phòng chat {GroupId} (IsLastOwner={IsLastOwner})", member.UserId, group.GroupId, isLastOwner);
        }

        // Chuyển quyền chủ phòng
        public Task TransferOwnershipAsync(ChatGroup group, GroupMember currentOwner, GroupMember newOwner, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation( "Orchestrator: Bắt đầu TransferOwnershipAsync trong phòng {GroupId} từ UserId={FromUser} sang UserId={ToUser}", group.GroupId, currentOwner.UserId, newOwner.UserId);

            // 1. DomainService: kiểm tra quyền chuyển owner
            _groupMemberDomainService.EnsureCanTransferOwnership(group, currentOwner, newOwner);

            // 2. Apply thay đổi
            currentOwner.IsOwner = false;
            currentOwner.CanControlFilm = false;

            newOwner.IsOwner = true;
            newOwner.CanControlFilm = true;

            _logger.LogInformation( "Orchestrator: Chuyển quyền chủ phòng {GroupId} thành công. NewOwner={NewOwner}", group.GroupId, newOwner.UserId);

            return Task.CompletedTask;
        }

        // Cập nhật quyền thành viên (mute, canChat, canControlFilm)
        public Task UpdatePermissionsAsync(ChatGroup group, GroupMember actingMember, GroupMember targetMember, GroupMemberUpdatePermissionsDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdatePermissionsAsync cho TargetUser={TargetUser} trong phòng {GroupId} bởi ActingUser={ActingUser}", targetMember.UserId, group.GroupId, actingMember.UserId);

            // 1. DomainService: kiểm tra actingMember có được chỉnh quyền targetMember không
            _groupMemberDomainService.EnsureCanUpdateMemberPermissions(group, actingMember, targetMember);

            // 2. Apply thay đổi từ DTO (chỉ update nếu có giá trị)
            if (dto.IsMuted.HasValue)
                targetMember.IsMuted = dto.IsMuted.Value;

            if (dto.CanChat.HasValue)
                targetMember.CanChat = dto.CanChat.Value;

            if (dto.CanControlFilm.HasValue)
                targetMember.CanControlFilm = dto.CanControlFilm.Value;

            _logger.LogInformation("Orchestrator: Đã cập nhật quyền cho TargetUser={TargetUser} trong phòng {GroupId}: IsMuted={IsMuted}, CanChat={CanChat}, CanControlFilm={CanControlFilm}", targetMember.UserId, group.GroupId, targetMember.IsMuted, targetMember.CanChat, targetMember.CanControlFilm);

            return Task.CompletedTask;
        }
    }
}
