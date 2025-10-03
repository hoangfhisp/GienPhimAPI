using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class TheTagService : ITheTagService
    {
        private readonly ITheTagOrchestrator _orchestrator;
        private readonly ITheTagResponseMapper _theTagResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TheTagService> _logger;

        public TheTagService(
            IUnitOfWork unitOfWork,
            ITheTagOrchestrator orchestrator,
            ITheTagResponseMapper theTagResponemapper,
            IPaginationMapper paginationMapper,
            ILogger<TheTagService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _theTagResponseMapper = theTagResponemapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<TheTagResponseDTO>>> GetAllTheTagsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllTheTagsAsync với page={Page}, limit={Limit}", page, limit);

            var (theTags, total, last) = await _unitOfWork.TheTags.GetAllTheTagsAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy TheTag nào");
                throw new TheTagNotFoundException();
            }

            var dtoList = _theTagResponseMapper.toDtoList(theTags);

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách TheTag thành công, tổng={Total}", total);

            return ResultListDTO<List<TheTagResponseDTO>>.Success(
                dtoList,
                "Lấy danh sách TheTag thành công",
                pagination
            );
        }

        public async Task<ResultDTO<TheTagResponseDTO>> GetDetailAsync(int theTagId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho TheTagId={TheTagId}", theTagId);

            var entity = await _unitOfWork.TheTags.GetByIdAsync(theTagId, ct);
            if (entity is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Thẻ Tag Id={TheTagId}", theTagId);
                throw new TheTagNotFoundException(theTagId.ToString());
            }

            var dto = _theTagResponseMapper.toDto(entity);

            _logger.LogInformation("ApplicationService: Lấy chi tiết Thẻ Tag Id={TheTagId} thành công", theTagId);

            return ResultDTO<TheTagResponseDTO>.Success(dto, "Lấy chi tiết thẻ tag thành công");
        }

        public async Task<ResultDTO> CreateTheTagAsync(TheTagCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu CreateTheTagAsync với tên {TenT}", createDto.TenT);

            var tag = await _orchestrator.CreateTheTagAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.TheTags.CreateAsync(tag, cancellationToken);
            if (result is null)
            {
                _logger.LogError("Service: Tạo TheTag {TenT} thất bại!", tag.TenT);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateTheTagFailedException(tag.TenT);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Service: Tạo TheTag {TenT} thành công", tag.TenT);

            return ResultDTO.Success($"Tạo thẻ tag {tag.TenT} thành công");
        }

        public async Task<ResultDTO<UpdateTheTagResponseDTO>> UpdateTheTagAsync(TheTagUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu UpdateTheTagAsync với Id={TheTagId}", updateDto.TheTagId);

            var tag = await _orchestrator.UpdateTheTagAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.TheTags.UpdateAsync(tag, cancellationToken);
            if (result is null)
            {
                _logger.LogError("Service: Cập nhật TheTag {TheTagId} thất bại!", updateDto.TheTagId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateTheTagFailedException(tag.TenT);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Service: Cập nhật TheTag {TheTagId} thành công", tag.TheTagId);

            var response = _theTagResponseMapper.ToUpdateDto(tag);

            return ResultDTO<UpdateTheTagResponseDTO>.Success(response, $"Cập nhật thẻ tag {tag.TenT} thành công");
        }

        public async Task<ResultDTO> DeleteTheTagAsync(TheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu DeleteTheTagAsync với Id={TheTagId}", deleteDto.TheTagId);

            await _orchestrator.DeleteTheTagAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var tag = await _unitOfWork.TheTags.GetByIdAsync(deleteDto.TheTagId, cancellationToken);
            if (tag is null)
            {
                _logger.LogWarning("Service: TheTag không tồn tại khi xóa Id={TheTagId}", deleteDto.TheTagId);
                return ResultDTO.Fail($"Không tìm thấy TheTag với Id {deleteDto.TheTagId}");
            }

            await _unitOfWork.TheTags.DeleteAsync(tag, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Service: Xóa TheTag {TheTagId} thành công", deleteDto.TheTagId);

            return ResultDTO.Success($"Xóa thẻ tag {tag.TenT} thành công");
        }
    }
}
