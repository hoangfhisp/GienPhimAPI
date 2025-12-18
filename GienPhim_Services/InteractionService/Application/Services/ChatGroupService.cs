using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Application.Exceptions.Duplicate;

namespace GienPhim_Services.InteractionService.Application.Services
{
    public class ChatGroupService : IChatGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatGroupOrchestrator _orchestrator;
        private readonly IChatGroupResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<ChatGroupService> _logger;

        public ChatGroupService(
            IUnitOfWork unitOfWork,
            IChatGroupOrchestrator orchestrator,
            IChatGroupResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ICurrentUserService currentUser,
            IDateTimeProvider clock,
            ILogger<ChatGroupService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _currentUser = currentUser;
            _clock = clock;
            _logger = logger;
        }

        // ================== QUERIES ==================

        public async Task<ResultDTO<ChatGroupResponseDTO>> GetDetailAsync(
            Guid groupId,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "ChatGroupService: Bắt đầu GetDetailAsync cho GroupId={GroupId}",
                groupId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning(
                    "ChatGroupService: Không tìm thấy phòng chat GroupId={GroupId}",
                    groupId);

                throw new ChatGroupNotFoundException();
            }

            var dto = _mapper.ToResponseDto(group);
            return ResultDTO<ChatGroupResponseDTO>.Success(
                dto,
                "Lấy chi tiết phòng chat thành công");
        }

        public async Task<ResultListDTO<List<ChatGroupListItemDTO>>> GetByFilmAsync(
            int phimId,
            string? status,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "ChatGroupService: GetByFilmAsync phimId={PhimId}, status={Status}, page={Page}",
                phimId, status, page);

            var (groups, total, last) = await _unitOfWork.ChatGroups.GetByFilmAsync(
                phimId, status, page, pageSize, cancellationToken);

            if (total == 0)
            {
                return ResultListDTO<List<ChatGroupListItemDTO>>.Success(
                    new List<ChatGroupListItemDTO>(),
                    "Không có phòng chat nào cho phim này.",
                    _paginationMapper.ToDto(page, pageSize, total, last));
            }

            var items = _mapper.ToListItemDtoList(groups);
            var pagination = _paginationMapper.ToDto(page, pageSize, total, last);

            return ResultListDTO<List<ChatGroupListItemDTO>>.Success(
                items,
                "Lấy danh sách phòng chat theo phim thành công",
                pagination);
        }

        public async Task<ResultListDTO<List<ChatGroupListItemDTO>>> GetMineAsync(
            string? role,
            string? status,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;

            _logger.LogInformation(
                "ChatGroupService: GetMineAsync userId={UserId}, role={Role}, status={Status}, page={Page}",
                userId, role, status, page);

            var (groups, total, last) = await _unitOfWork.ChatGroups.GetMineAsync(
                userId, role, status, page, pageSize, cancellationToken);

            var items = _mapper.ToListItemDtoList(groups);
            var pagination = _paginationMapper.ToDto(page, pageSize, total, last);

            return ResultListDTO<List<ChatGroupListItemDTO>>.Success(
                items,
                "Lấy danh sách phòng chat của tôi thành công",
                pagination);
        }

        // ================== COMMANDS ==================
        public async Task<ResultDTO<ChatGroupResponseDTO>> CreateChatGroupAsync(ChatGroupCreateDTO dto, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("ChatGroupService: Bắt đầu CreateChatGroupAsync PhimId={PhimId} bởi UserId={UserId}", dto.PhimId, userId);

            // RULE NGHIỆP VỤ SENIOR YÊU CẦU
            // 1 user chỉ được tạo 1 phòng xem chung cho 1 phim tại 1 thời điểm
            var hasActiveGroup = await _unitOfWork.ChatGroups
                .ExistsActiveGroupByOwnerAndFilmAsync(
                    userId,
                    dto.PhimId,
                    now,
                    cancellationToken);

            if (hasActiveGroup)
            {
                _logger.LogWarning("ChatGroupService: UserId={UserId} đã có phòng chat đang hoạt động cho PhimId={PhimId}", userId, dto.PhimId);

                throw new ChatGroupAlreadyExistsException("Bạn đã có một phòng xem chung cho phim này đang hoạt động.");
            }

            // Orchestrator: dựng entity + domain validation
            var group = await _orchestrator.CreateChatGroupAsync(dto, userId, now, cancellationToken);

            await using var transaction =
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.ChatGroups.CreateAsync(group, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ChatGroupService: Tạo phòng chat {GroupId} thành công!", group.GroupId);

            var response = _mapper.ToResponseDto(group);
            return ResultDTO<ChatGroupResponseDTO>.Success(response, "Tạo phòng chat thành công");
        }

        // CẬP NHẬT CÀI ĐẶT
        public async Task<ResultDTO<ChatGroupResponseDTO>> UpdateSettingsAsync(
            Guid groupId,
            ChatGroupUpdateSettingsDTO dto,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation(
                "ChatGroupService: UpdateSettingsAsync GroupId={GroupId}, UserId={UserId}",
                groupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning(
                    "ChatGroupService: Không tìm thấy phòng chat GroupId={GroupId} để cập nhật settings",
                    groupId);

                throw new ChatGroupNotFoundException();
            }

            // Orchestrator sẽ tự gọi domain & có thể ném DomainException hoặc ApplicationException
            await _orchestrator.UpdateSettingsAsync(group, dto, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.ChatGroups.UpdateAsync(group, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToResponseDto(group);
            return ResultDTO<ChatGroupResponseDTO>.Success(
                response,
                "Cập nhật cài đặt phòng chat thành công");
        }

        public async Task<ResultDTO<ChatGroupResponseDTO>> RefreshInviteCodeAsync(
            Guid groupId,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation(
                "ChatGroupService: RefreshInviteCodeAsync GroupId={GroupId}, UserId={UserId}",
                groupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning(
                    "ChatGroupService: Không tìm thấy phòng chat GroupId={GroupId} để làm mới mã mời",
                    groupId);

                throw new ChatGroupNotFoundException();
            }

            await _orchestrator.RefreshInviteCodeAsync(group, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.ChatGroups.UpdateAsync(group, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToResponseDto(group);
            return ResultDTO<ChatGroupResponseDTO>.Success(
                response,
                "Làm mới mã mời thành công");
        }

        public async Task<ResultDTO<ChatGroupResponseDTO>> DisableInviteCodeAsync(
            Guid groupId,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation(
                "ChatGroupService: DisableInviteCodeAsync GroupId={GroupId}, UserId={UserId}",
                groupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning(
                    "ChatGroupService: Không tìm thấy phòng chat GroupId={GroupId} để tắt mã mời",
                    groupId);

                throw new ChatGroupNotFoundException();
            }

            await _orchestrator.DisableInviteCodeAsync(group, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.ChatGroups.UpdateAsync(group, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToResponseDto(group);
            return ResultDTO<ChatGroupResponseDTO>.Success(
                response,
                "Tắt mã mời thành công");
        }

        public async Task<ResultDTO<ChatGroupResponseDTO>> CloseChatGroupAsync(
            Guid groupId,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation(
                "ChatGroupService: CloseChatGroupAsync GroupId={GroupId}, UserId={UserId}",
                groupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning(
                    "ChatGroupService: Không tìm thấy phòng chat GroupId={GroupId} để đóng",
                    groupId);

                throw new ChatGroupNotFoundException();
            }

            await _orchestrator.CloseChatGroupAsync(group, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.ChatGroups.UpdateAsync(group, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToResponseDto(group);
            return ResultDTO<ChatGroupResponseDTO>.Success(
                response,
                "Đóng phòng chat thành công");
        }
    }
}