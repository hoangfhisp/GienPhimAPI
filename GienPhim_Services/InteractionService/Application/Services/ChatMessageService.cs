using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Application.Exceptions.Permission;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.InteractionService.Application.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatMessageOrchestrator _orchestrator;
        private readonly IChatMessageResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<ChatMessageService> _logger;

        public ChatMessageService(
            IUnitOfWork unitOfWork,
            IChatMessageOrchestrator orchestrator,
            IChatMessageResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ICurrentUserService currentUser,
            IDateTimeProvider clock,
            ILogger<ChatMessageService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _currentUser = currentUser;
            _clock = clock;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<ChatMessageResponseDTO>>> GetMessagesByGroupAsync(Guid groupId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;

            _logger.LogInformation("ChatMessageService: GetMessagesByGroupAsync GroupId={GroupId}, UserId={UserId}, Page={Page}, PageSize={PageSize}", groupId, userId, page, pageSize);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(groupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("ChatMessageService: Không tìm thấy phòng chat GroupId={GroupId} khi lấy lịch sử tin nhắn", groupId);

                throw new ChatGroupNotFoundException();
            }

            // Chỉ cho phép member xem lịch sử
            var member = await _unitOfWork.GroupMembers.GetByGroupAndUserAsync(groupId, userId, cancellationToken);

            if (member is null)
            {
                _logger.LogWarning("ChatMessageService: UserId={UserId} không phải thành viên GroupId={GroupId}, không thể xem lịch sử tin nhắn", userId, groupId);

                throw new ChatMessagePermissionException("Bạn không phải thành viên của phòng chat này, không thể xem lịch sử tin nhắn.");
            }

            var (items, total, lastPage) = await _unitOfWork.ChatMessages.GetByGroupAsync(groupId, page, pageSize, cancellationToken);

            var dtos = _mapper.ToResponseDtoList(items);
            var pagination = _paginationMapper.ToDto(page, pageSize, total, lastPage);

            _logger.LogInformation("ChatMessageService: Lấy lịch sử tin nhắn GroupId={GroupId} thành công. Total={Total}", groupId, total);

            return ResultListDTO<List<ChatMessageResponseDTO>>.Success(dtos, "Lấy danh sách tin nhắn thành công", pagination);
        }

        public async Task<ResultDTO<ChatMessageResponseDTO>> SendMessageAsync(ChatMessageCreateDTO dto, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            _logger.LogInformation("ChatMessageService: SendMessageAsync GroupId={GroupId}, UserId={UserId}", dto.GroupId, userId);

            var group = await _unitOfWork.ChatGroups.GetByIdAsync(dto.GroupId, cancellationToken);
            if (group is null)
            {
                _logger.LogWarning("ChatMessageService: Không tìm thấy phòng chat GroupId={GroupId} khi gửi tin nhắn", dto.GroupId);

                throw new ChatGroupNotFoundException();
            }

            // Lấy thông tin member (có thể null, domain sẽ xử lý rule)
            var member = await _unitOfWork.GroupMembers.GetByGroupAndUserAsync(dto.GroupId, userId, cancellationToken);

            // Orchestrator: domain rule + dựng ChatMessage
            var message = await _orchestrator.CreateUserMessageAsync(group, member, dto, userId, now, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.ChatMessages.AddAsync(message, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ChatMessageService: UserId={UserId} đã gửi tin nhắn vào GroupId={GroupId} thành công. MessageId={MessageId}", userId, dto.GroupId, message.MessageId);

            var response = _mapper.ToResponseDto(message);
            return ResultDTO<ChatMessageResponseDTO>.Success(response, "Gửi tin nhắn thành công");
        }
    }
}
