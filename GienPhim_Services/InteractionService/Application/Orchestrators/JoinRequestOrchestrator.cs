using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Orchestrators
{
    public class JoinRequestOrchestrator : IJoinRequestOrchestrator
    {
        private readonly IJoinRequestDomainService _joinRequestDomainService;
        private readonly IGroupMemberDomainService _groupMemberDomainService;
        private readonly IJoinRequestFactory _joinRequestFactory;
        private readonly IGroupMemberFactory _groupMemberFactory;
        private readonly IJoinRequestRepository _joinRequestRepository;
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly ILogger<JoinRequestOrchestrator> _logger;

        public JoinRequestOrchestrator(
            IJoinRequestDomainService joinRequestDomainService,
            IGroupMemberDomainService groupMemberDomainService,
            IJoinRequestFactory joinRequestFactory,
            IGroupMemberFactory groupMemberFactory,
            IJoinRequestRepository joinRequestRepository,
            IGroupMemberRepository groupMemberRepository,
            ILogger<JoinRequestOrchestrator> logger)
        {
            _joinRequestDomainService = joinRequestDomainService;
            _groupMemberDomainService = groupMemberDomainService;
            _joinRequestFactory = joinRequestFactory;
            _groupMemberFactory = groupMemberFactory;
            _joinRequestRepository = joinRequestRepository;
            _groupMemberRepository = groupMemberRepository;
            _logger = logger;
        }

        // Tạo yêu cầu tham gia mới
        public async Task<JoinRequest> CreateJoinRequestAsync(ChatGroup group, JoinRequestCreateDTO dto, string currentUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateJoinRequestAsync cho GroupId={GroupId}, UserId={UserId}", dto.GroupId, currentUserId);

            // 1. kiểm tra user đã là member chưa
            var existingMember = await _groupMemberRepository.GetByGroupAndUserAsync(dto.GroupId, currentUserId, cancellationToken);

            var alreadyMember = existingMember is not null;

            // 2. kiếm request Pending hiện tại
            var existingPending = await _joinRequestRepository.GetPendingByGroupAndUserAsync(dto.GroupId, currentUserId, cancellationToken);

            // 3. domain rule
            _joinRequestDomainService.EnsureCanCreateRequest(group, existingPending, alreadyMember, nowUtc);

            // 4. factory dựng entity
            var request = _joinRequestFactory.CreateJoinRequest(dto, currentUserId, nowUtc);

            _logger.LogInformation("Orchestrator: Yêu cầu tham gia phòng chat GroupId={GroupId} của UserId={UserId} đã được khởi tạo & validate thành công.", dto.GroupId, currentUserId);

            return request;
        }

        // Chấp nhận yêu cầu (Accept)
        public async Task<GroupMember?> AcceptAsync(JoinRequest request, ChatGroup group, string actingUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Accept JoinRequest RequestId={RequestId}, GroupId={GroupId}, ActingUser={ActingUser}", request.RequestId, request.GroupId, actingUserId);

            // 1. domain rule: actingUser có quyền, request còn Pending, group còn hợp lệ
            _joinRequestDomainService.EnsureCanProcessRequest(request, group, actingUserId, nowUtc, action: "Accept");

            // 2. đánh dấu request đã xử lý
            request.Status = "Accepted";
            request.ProcessedAt = nowUtc;

            // 3. kiểm tra user đã là member chưa
            var existingMember = await _groupMemberRepository.GetByGroupAndUserAsync(request.GroupId, request.RequestUserId, cancellationToken);

            if (existingMember is not null)
            {
                _logger.LogInformation("Orchestrator: UserId={UserId} đã là thành viên của GroupId={GroupId}, không tạo GroupMember mới.", request.RequestUserId, request.GroupId);

                _logger.LogInformation("Orchestrator: Accept JoinRequest RequestId={RequestId} hoàn tất (đã update status).", request.RequestId);

                return null;
            }

            // 4. domain rule Join member (tận dụng rule của GroupMember)
            _groupMemberDomainService.EnsureCanJoin(group, existingMember, nowUtc);

            // 5. tạo GroupMember mới (không phải owner)
            var member = _groupMemberFactory.CreateForJoin(group, request.RequestUserId, nowUtc, isOwner: false);

            _logger.LogInformation("Orchestrator: Accept JoinRequest RequestId={RequestId} thành công, tạo mới GroupMember UserId={UserId} cho GroupId={GroupId}.", request.RequestId, request.RequestUserId, request.GroupId);

            return member;
        }

        // Từ chối yêu cầu (Reject)
        public Task RejectAsync(JoinRequest request, ChatGroup group, string actingUserId, DateTime nowUtc, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Reject JoinRequest RequestId={RequestId}, GroupId={GroupId}, ActingUser={ActingUser}", request.RequestId, request.GroupId, actingUserId);

            // 1. domain rule
            _joinRequestDomainService.EnsureCanProcessRequest(request ,group, actingUserId, nowUtc, action: "Reject");

            // 2. update trạng thái
            request.Status = "Rejected";
            request.ProcessedAt = nowUtc;

            _logger.LogInformation("Orchestrator: Reject JoinRequest RequestId={RequestId} thành công (Status={Status}).", request.RequestId, request.Status);

            return Task.CompletedTask;
        }
    }
}