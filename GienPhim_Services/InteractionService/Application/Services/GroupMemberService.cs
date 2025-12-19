using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using GienPhim_Services.InteractionService.Application.Exceptions.NotFounds;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Services
{
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupMemberOrchestrator _orchestrator;
        private readonly IGroupMemberResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<GroupMemberService> _logger;

        public GroupMemberService(
            IUnitOfWork unitOfWork,
            IGroupMemberOrchestrator orchestrator,
            IGroupMemberResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ICurrentUserService currentUser,
            IDateTimeProvider clock,
            ILogger<GroupMemberService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _currentUser = currentUser;
            _clock = clock;
            _logger = logger;
        }

        // Lấy danh sách thành viên trong 1 phòng chat
        public async Task<ResultListDTO<List<GroupMemberResponseDTO>>> GetMembersByGroupAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("GroupMemberService: GetMembersByGroupAsync GroupId={GroupId}, Page={Page}, PageSize={PageSize}", groupId, page, pageSize);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("GroupMemberService: Không tìm thấy phòng chat GroupId={GroupId} khi lấy danh sách thành viên", groupId);

                throw new ChatGroupNotFoundException();
            }

            var (members, total, lastPage) = await _unitOfWork.GroupMembers.GetByGroupPagedAsync(groupId, page, pageSize, cancellationToken);

            var items = _mapper.ToResponseDtoList(members);
            var pagination = _paginationMapper.ToDto(page, pageSize, total, lastPage);

            _logger.LogInformation("GroupMemberService: Lấy danh sách thành viên GroupId={GroupId} thành công. Total={Total}", groupId, total);

            return ResultListDTO<List<GroupMemberResponseDTO>>.Success(items, "Lấy danh sách thành viên phòng chat thành công", pagination);
        }

        // User tham gia phòng chat
        public async Task<ResultDTO<GroupMemberResponseDTO>> JoinGroupAsync(GroupMemberJoinDTO dto, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("GroupMemberService: JoinGroupAsync GroupId={GroupId}, UserId={UserId}", dto.GroupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(dto.GroupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("GroupMemberService: Không tìm thấy phòng chat GroupId={GroupId} để tham gia", dto.GroupId);

                throw new ChatGroupNotFoundException();
            }

            // Orchestrator: validate + dựng entity GroupMember
            var member = await _orchestrator.JoinAsync(group, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.GroupMembers.AddAsync(member, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("GroupMemberService: UserId={UserId} tham gia phòng chat GroupId={GroupId} thành công", userId, dto.GroupId);

            var response = _mapper.ToResponseDto(member);
            return ResultDTO<GroupMemberResponseDTO>.Success(response, "Tham gia phòng chat thành công");
        }

        // User rời phòng chat (leave)
        public async Task<ResultDTO<GroupMemberResponseDTO>> LeaveGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("GroupMemberService: LeaveGroupAsync GroupId={GroupId}, UserId={UserId}", groupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("GroupMemberService: Không tìm thấy phòng chat GroupId={GroupId} để rời", groupId);

                throw new ChatGroupNotFoundException();
            }

            var member = await _unitOfWork.GroupMembers.GetByGroupAndUserAsync(groupId, userId, cancellationToken);

            if (member is null)
            {
                _logger.LogWarning("GroupMemberService: UserId={UserId} không phải thành viên của GroupId={GroupId} khi gọi LeaveGroup", userId, groupId);

                throw new GroupMemberNotFoundException("Bạn không phải thành viên của phòng chat này.");
            }

            // Orchestrator: check rule (owner cuối, v.v.)
            await _orchestrator.LeaveAsync(group, member, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.GroupMembers.RemoveAsync(member, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("GroupMemberService: UserId={UserId} đã rời phòng chat GroupId={GroupId}", userId, groupId);

            var response = _mapper.ToResponseDto(member);
            return ResultDTO<GroupMemberResponseDTO>.Success(response, "Rời phòng chat thành công");
        }

        // Cập nhật quyền thành viên (mute, canChat, canControlFilm)
        public async Task<ResultDTO<GroupMemberResponseDTO>> UpdatePermissionsAsync(Guid groupId, string targetUserId, GroupMemberUpdatePermissionsDTO dto, CancellationToken cancellationToken = default)
        {
            var actingUserId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("GroupMemberService: UpdatePermissionsAsync GroupId={GroupId}, ActingUser={ActUser}, TargetUser={TargetUser}", groupId, actingUserId, targetUserId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("GroupMemberService: Không tìm thấy phòng chat GroupId={GroupId} để cập nhật quyền thành viên", groupId);

                throw new ChatGroupNotFoundException();
            }

            var actingMember = await _unitOfWork.GroupMembers.GetByGroupAndUserAsync(groupId, actingUserId, cancellationToken);
            if (actingMember is null)
            {
                _logger.LogWarning("GroupMemberService: ActingUser={UserId} không phải thành viên GroupId={GroupId}", actingUserId, groupId);

                throw new GroupMemberNotFoundException("Bạn không phải thành viên của phòng chat này.");
            }

            var targetMember = await _unitOfWork.GroupMembers.GetByGroupAndUserAsync(groupId, targetUserId, cancellationToken);
            if (targetMember is null)
            {
                _logger.LogWarning("GroupMemberService: TargetUser={UserId} không phải thành viên GroupId={GroupId}", targetUserId, groupId);

                throw new GroupMemberNotFoundException("Không tìm thấy thành viên cần cập nhật quyền.");
            }

            // Orchestrator: check quyền + apply DTO lên entity
            await _orchestrator.UpdatePermissionsAsync(group, actingMember, targetMember, dto, actingUserId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.GroupMembers.UpdateAsync(targetMember, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("GroupMemberService: Cập nhật quyền cho TargetUser={TargetUser} trong GroupId={GroupId} thành công", targetUserId, groupId);

            var response = _mapper.ToResponseDto(targetMember);
            return ResultDTO<GroupMemberResponseDTO>.Success(response, "Cập nhật quyền thành viên thành công");
        }

        // Chuyển quyền chủ phòng
        public async Task<ResultDTO<GroupMemberResponseDTO>> TransferOwnershipAsync(Guid groupId, string newOwnerUserId, CancellationToken cancellationToken = default)
        {
            var actingUserId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("GroupMemberService: TransferOwnershipAsync GroupId={GroupId}, ActingUser={ActUser}, NewOwner={NewOwner}", groupId, actingUserId, newOwnerUserId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("GroupMemberService: Không tìm thấy phòng chat GroupId={GroupId} để chuyển quyền chủ phòng", groupId);

                throw new ChatGroupNotFoundException();
            }

            var currentOwner = await _unitOfWork.GroupMembers
                .GetByGroupAndUserAsync(groupId, actingUserId, cancellationToken);
            if (currentOwner is null)
            {
                _logger.LogWarning("GroupMemberService: ActingUser={UserId} không phải thành viên GroupId={GroupId} khi chuyển owner", actingUserId, groupId);

                throw new GroupMemberNotFoundException("Bạn không phải thành viên của phòng chat này.");
            }

            var newOwner = await _unitOfWork.GroupMembers
                .GetByGroupAndUserAsync(groupId, newOwnerUserId, cancellationToken);
            if (newOwner is null)
            {
                _logger.LogWarning("GroupMemberService: NewOwner={UserId} không phải thành viên GroupId={GroupId}", newOwnerUserId, groupId);

                throw new GroupMemberNotFoundException("Không tìm thấy thành viên để chuyển quyền chủ phòng.");
            }

            // Orchestrator: validate + apply chuyển owner
            await _orchestrator.TransferOwnershipAsync(group, currentOwner, newOwner, actingUserId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.GroupMembers.UpdateAsync(currentOwner, cancellationToken);
            await _unitOfWork.GroupMembers.UpdateAsync(newOwner, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("GroupMemberService: Chuyển quyền chủ phòng GroupId={GroupId} từ UserId={FromUser} sang UserId={ToUser} thành công", groupId, actingUserId, newOwnerUserId);

            var response = _mapper.ToResponseDto(newOwner);
            return ResultDTO<GroupMemberResponseDTO>.Success(response, "Chuyển quyền chủ phòng thành công");
        }
    }
}