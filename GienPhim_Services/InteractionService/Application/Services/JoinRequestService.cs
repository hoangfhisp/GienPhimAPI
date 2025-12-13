using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Services
{
    public class JoinRequestService : IJoinRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJoinRequestOrchestrator _orchestrator;
        private readonly IJoinRequestResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<JoinRequestService> _logger;

        public JoinRequestService(
            IUnitOfWork unitOfWork,
            IJoinRequestOrchestrator orchestrator,
            IJoinRequestResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ICurrentUserService currentUser,
            IDateTimeProvider clock,
            ILogger<JoinRequestService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _currentUser = currentUser;
            _clock = clock;
            _logger = logger;
        }

        // Lấy danh sách yêu cầu tham gia theo Group
        public async Task<ResultListDTO<List<JoinRequestResponseDTO>>> GetByGroupAsync(Guid groupId, string? status, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("JoinRequestService: GetByGroupAsync GroupId={GroupId}, Status={Status}, Page={Page}, PageSize={PageSize}", groupId, status, page, pageSize);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("JoinRequestService: Không tìm thấy phòng chat GroupId={GroupId} khi lấy danh sách yêu cầu tham gia", groupId);

                throw new ChatGroupNotFoundException();
            }

            var (items, total, lastPage) = await _unitOfWork.JoinRequests.GetByGroupAsync(groupId, status, page, pageSize, cancellationToken);

            var dtos = _mapper.ToResponseDtoList(items);
            var pagination = _paginationMapper.ToDto(page, pageSize, total, lastPage);

            _logger.LogInformation("JoinRequestService: Lấy danh sách yêu cầu tham gia GroupId={GroupId} thành công. Total={Total}", groupId, total);

            return ResultListDTO<List<JoinRequestResponseDTO>>.Success(dtos, "Lấy danh sách yêu cầu tham gia phòng chat thành công", pagination);
        }

        // User gửi yêu cầu tham gia
        public async Task<ResultDTO<JoinRequestResponseDTO>> CreateJoinRequestAsync(JoinRequestCreateDTO dto, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("JoinRequestService: CreateJoinRequestAsync GroupId={GroupId}, UserId={UserId}", dto.GroupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(dto.GroupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("JoinRequestService: Không tìm thấy phòng chat GroupId={GroupId} khi gửi yêu cầu tham gia", dto.GroupId);

                throw new ChatGroupNotFoundException();
            }

            // Orchestrator: domain rule + dựng JoinRequest
            var request = await _orchestrator.CreateJoinRequestAsync(group, dto, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.JoinRequests.AddAsync(request, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("JoinRequestService: UserId={UserId} đã gửi yêu cầu tham gia phòng chat GroupId={GroupId} thành công", userId, dto.GroupId);

            var response = _mapper.ToResponseDto(request);
            return ResultDTO<JoinRequestResponseDTO>.Success(response, "Gửi yêu cầu tham gia phòng chat thành công");
        }

        // Chủ phòng chấp nhận yêu cầu
        public async Task<ResultDTO<JoinRequestResponseDTO>> AcceptAsync(Guid requestId, CancellationToken cancellationToken = default)
        {
            var actingUserId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("JoinRequestService: AcceptAsync RequestId={RequestId}, ActingUser={UserId}", requestId, actingUserId);

            var request = await _unitOfWork.JoinRequests.GetByIdAsync(requestId, cancellationToken);
            if (request is null)
            {
                _logger.LogWarning("JoinRequestService: Không tìm thấy JoinRequest RequestId={RequestId} khi Accept", requestId);

                throw new JoinRequestNotFoundException();
            }

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(request.GroupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("JoinRequestService: Không tìm thấy phòng chat GroupId={GroupId} khi Accept JoinRequest RequestId={RequestId}", request.GroupId, requestId);

                throw new ChatGroupNotFoundException();
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Orchestrator: check rule + update request + tạo GroupMember nếu cần
            var newMember = await _orchestrator.AcceptAsync(request, group, actingUserId, now, cancellationToken);

            // Lưu JoinRequest
            await _unitOfWork.JoinRequests.UpdateAsync(request, cancellationToken);

            // Nếu có member mới thì lưu
            if (newMember is not null)
            {
                await _unitOfWork.GroupMembers.AddAsync(newMember, cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("JoinRequestService: Accept JoinRequest RequestId={RequestId} thành công.", requestId);

            var response = _mapper.ToResponseDto(request);
            return ResultDTO<JoinRequestResponseDTO>.Success(response, "Chấp nhận yêu cầu tham gia phòng chat thành công");
        }

        // Chủ phòng từ chối yêu cầu
        public async Task<ResultDTO<JoinRequestResponseDTO>> RejectAsync(Guid requestId, CancellationToken cancellationToken = default)
        {
            var actingUserId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("JoinRequestService: RejectAsync RequestId={RequestId}, ActingUser={UserId}", requestId, actingUserId);

            var request = await _unitOfWork.JoinRequests.GetByIdAsync(requestId, cancellationToken);
            if (request is null)
            {
                _logger.LogWarning("JoinRequestService: Không tìm thấy JoinRequest RequestId={RequestId} khi Reject", requestId);

                throw new JoinRequestNotFoundException();
            }

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(request.GroupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("JoinRequestService: Không tìm thấy phòng chat GroupId={GroupId} khi Reject JoinRequest RequestId={RequestId}", request.GroupId, requestId);

                throw new ChatGroupNotFoundException();
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Orchestrator: check rule + update trạng thái request
            await _orchestrator.RejectAsync(request, group, actingUserId, now, cancellationToken);

            await _unitOfWork.JoinRequests.UpdateAsync(request, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("JoinRequestService: Reject JoinRequest RequestId={RequestId} thành công.", requestId);

            var response = _mapper.ToResponseDto(request);
            return ResultDTO<JoinRequestResponseDTO>.Success(response, "Từ chối yêu cầu tham gia phòng chat thành công");
        }
    }
}
