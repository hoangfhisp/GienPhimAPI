using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class PhanLoaiTheTagService : IPhanLoaiTheTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhanLoaiTheTagResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IPhanLoaiTheTagFactory _factory;
        private readonly IPhanLoaiTheTagDomainService _domainService;
        private readonly ILogger<PhanLoaiTheTagService> _logger;

        public PhanLoaiTheTagService(
            IUnitOfWork unitOfWork,
            IPhanLoaiTheTagResponseMapper mapper,
            IPaginationMapper paginationMapper,
            IPhanLoaiTheTagFactory factory,
            IPhanLoaiTheTagDomainService domainService,
            ILogger<PhanLoaiTheTagService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<PhanLoaiTheTagResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu GetAllAsync page={Page}, limit={Limit}", page, limit);

            var (items, total, last) = await _unitOfWork.PhanLoaiTheTags.GetAllAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("Service: Không tìm thấy PhanLoaiTheTag nào");
                throw new PhanLoaiTheTagNotFoundException();
            }

            var dtoList = _mapper.ToDtoList(items);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<PhanLoaiTheTagResponseDTO>>.Success(dtoList, "Lấy danh sách thành công", pagination);
        }

        public async Task<ResultDTO> CreateAsync(PhanLoaiTheTagCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu CreateAsync PhimId={PhimId}, TheTagId={TheTagId}", createDto.PhimId, createDto.TheTagId);

            var phanLoaiTheTag = _factory.Create(createDto);

            var existing = (await _unitOfWork.PhanLoaiTheTags.GetAllAsync(1, int.MaxValue, cancellationToken)).phanLoaiTheTags;
            _domainService.EnsureCanCreate(phanLoaiTheTag, existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.PhanLoaiTheTags.CreateAsync(phanLoaiTheTag, cancellationToken);
            if (result == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new CreatePhanLoaiTheTagFailedException(createDto.PhimId, createDto.TheTagId);
            }

            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Tạo quan hệ PhimId={createDto.PhimId}, TheTagId={createDto.TheTagId} thành công");
        }

        public async Task<ResultDTO<UpdatePhanLoaiTheTagResponseDTO>> UpdateAsync(PhanLoaiTheTagUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu UpdateAsync PhimId={PhimId}, TheTagId={TheTagId}", updateDto.PhimId, updateDto.TheTagId);

            var existing = await _unitOfWork.PhanLoaiTheTags.GetByIdAsync(updateDto.PhimId, updateDto.TheTagId, cancellationToken);
            if (existing == null)
            {
                throw new PhanLoaiTheTagNotFoundException(updateDto.PhimId, updateDto.TheTagId);
            }

            var updatedEntity = _factory.Update(updateDto);
            _domainService.EnsureCanUpdate(updatedEntity, existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.PhanLoaiTheTags.UpdateAsync(updatedEntity, cancellationToken);
            if (result == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdatePhanLoaiTheTagFailedException(updateDto.PhimId, updateDto.TheTagId);
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToUpdateDto(result);
            return ResultDTO<UpdatePhanLoaiTheTagResponseDTO>.Success(response, "Cập nhật quan hệ thành công");
        }

        public async Task<ResultDTO> DeleteAsync(PhanLoaiTheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu DeleteAsync PhimId={PhimId}, TheTagId={TheTagId}", deleteDto.PhimId, deleteDto.TheTagId);

            var existing = await _unitOfWork.PhanLoaiTheTags.GetByIdAsync(deleteDto.PhimId, deleteDto.TheTagId, cancellationToken);
            if (existing == null)
            {
                throw new PhanLoaiTheTagNotFoundException(deleteDto.PhimId, deleteDto.TheTagId);
            }

            _domainService.EnsureCanDelete(existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.PhanLoaiTheTags.DeleteAsync(existing, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Xoá quan hệ PhimId={deleteDto.PhimId}, TheTagId={deleteDto.TheTagId} thành công");
        }
    }
}
