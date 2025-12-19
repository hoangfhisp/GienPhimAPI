using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using GienPhim_Services.FilmService.Application.Exceptions.Duplicates;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class PhanLoaiTheLoaiService : IPhanLoaiTheLoaiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhanLoaiTheLoaiResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IPhanLoaiTheLoaiFactory _factory;
        private readonly IPhanLoaiTheLoaiDomainService _domainService;
        private readonly ILogger<PhanLoaiTheLoaiService> _logger;

        public PhanLoaiTheLoaiService(
            IUnitOfWork unitOfWork,
            IPhanLoaiTheLoaiResponseMapper mapper,
            IPaginationMapper paginationMapper,
            IPhanLoaiTheLoaiFactory factory,
            IPhanLoaiTheLoaiDomainService domainService,
            ILogger<PhanLoaiTheLoaiService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<PhanLoaiTheLoaiResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: GetAll PhanLoaiTheLoai page={Page}, limit={Limit}", page, limit);

            var (phanLoaiTheLoais, total, last) = await _unitOfWork.PhanLoaiTheLoais.GetAllPhanLoaiTheLoaisAsync(page, limit, cancellationToken);

            if (total == 0)
                throw new PhanLoaiTheLoaiNotFoundException();

            var dtoList = _mapper.ToDtoList(phanLoaiTheLoais);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<PhanLoaiTheLoaiResponseDTO>>.Success(dtoList, "Lấy danh sách thành công", pagination);
        }

        public async Task<ResultDTO> CreateAsync(PhanLoaiTheLoaiCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Create PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId}", createDto.PhimId, createDto.TheLoaiId);

            var entity = _factory.Create(createDto);

            var existing = await _unitOfWork.PhanLoaiTheLoais.GetByIdAsync(createDto.PhimId, createDto.TheLoaiId, cancellationToken);
            if (existing != null)
                throw new DuplicatePhanLoaiTheLoaiException(createDto.PhimId, createDto.TheLoaiId);

            _domainService.EnsureCanCreate(entity, new List<Domain.Entities.PhanLoaiTheLoai>());

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.PhanLoaiTheLoais.CreateAsync(entity, cancellationToken);
            if (result == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new CreatePhanLoaiTheLoaiFailedException(createDto.PhimId, createDto.TheLoaiId);
            }

            await transaction.CommitAsync(cancellationToken);
            return ResultDTO.Success($"Tạo quan hệ PhimId={createDto.PhimId}, TheLoaiId={createDto.TheLoaiId} thành công");
        }

        public async Task<ResultDTO<UpdatePhanLoaiTheLoaiResponseDTO>> UpdateAsync(PhanLoaiTheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Update PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId}", updateDto.PhimId, updateDto.TheLoaiId);

            var existing = await _unitOfWork.PhanLoaiTheLoais.GetByIdAsync(updateDto.PhimId, updateDto.TheLoaiId, cancellationToken);
            if (existing == null)
                throw new PhanLoaiTheLoaiNotFoundException(updateDto.PhimId, updateDto.TheLoaiId);

            var updatedEntity = _factory.Update(updateDto);

            _domainService.EnsureCanUpdate(updatedEntity, existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.PhanLoaiTheLoais.UpdateAsync(updatedEntity, cancellationToken);
            if (result == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdatePhanLoaiTheLoaiFailedException(updateDto.PhimId, updateDto.TheLoaiId);
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToUpdateDto(updatedEntity);
            return ResultDTO<UpdatePhanLoaiTheLoaiResponseDTO>.Success(response, "Cập nhật quan hệ thành công");
        }

        public async Task<ResultDTO> DeleteAsync(PhanLoaiTheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Delete PhanLoaiTheLoai PhimId={PhimId}, TheLoaiId={TheLoaiId}", deleteDto.PhimId, deleteDto.TheLoaiId);

            var existing = await _unitOfWork.PhanLoaiTheLoais.GetByIdAsync(deleteDto.PhimId, deleteDto.TheLoaiId, cancellationToken);
            if (existing == null)
                throw new PhanLoaiTheLoaiNotFoundException(deleteDto.PhimId, deleteDto.TheLoaiId);

            _domainService.EnsureCanDelete(existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.PhanLoaiTheLoais.DeleteAsync(existing, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Xóa quan hệ PhimId={deleteDto.PhimId}, TheLoaiId={deleteDto.TheLoaiId} thành công");
        }
    }
}
