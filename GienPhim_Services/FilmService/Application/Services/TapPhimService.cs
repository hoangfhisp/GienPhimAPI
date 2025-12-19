using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class TapPhimService : ITapPhimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITapPhimResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ITapPhimFactory _factory;
        private readonly ITapPhimDomainService _domainService;
        private readonly ILogger<TapPhimService> _logger;

        public TapPhimService(
            IUnitOfWork unitOfWork,
            ITapPhimResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ITapPhimFactory factory,
            ITapPhimDomainService domainService,
            ILogger<TapPhimService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        // Lấy danh sách
        public async Task<ResultListDTO<List<TapPhimResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAll TapPhim page={Page}, limit={Limit}", page, limit);

            var (list, total, last) = await _unitOfWork.TapPhims.GetAllAsync(page, limit, ct);
            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy TapPhim nào");
                throw new TapPhimNotFoundException();
            }

            var resultDtos = _mapper.ToDtoList(list);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách TapPhim thành công, tổng={Total}", total);

            return ResultListDTO<List<TapPhimResponseDTO>>.Success(resultDtos, "Lấy danh sách tập phim thành công", pagination);
        }

        public async Task<ResultListDTO<List<TapPhimResponseDTO>>> GetByPhimIdAsync(int phimId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetByPhimIdAsync phimId={PhimId}", phimId);

            var tapPhims = await _unitOfWork.TapPhims.GetByPhimIdAsync(phimId, cancellationToken);

            if (!tapPhims.Any())
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy tập nào cho PhimId={PhimId}", phimId);
                throw new TapPhimNotFoundException(phimId);
            }

            var resultDtos = _mapper.ToDtoList(tapPhims);

            _logger.LogInformation("ApplicationService: Lấy danh sách tập theo PhimId={PhimId} thành công, tổng={Total}", phimId, resultDtos.Count);

            return ResultListDTO<List<TapPhimResponseDTO>>.Success(resultDtos, $"Lấy danh sách tập phim theo PhimId={phimId} thành công");
        }

        // Tạo tập phim mới
        public async Task<ResultDTO> CreateAsync(TapPhimCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu Create TapPhim cho PhimId={PhimId}", dto.PhimId);

            string videoPath = string.Empty;

            // Lấy phim để tạo tên file chuẩn
            var phim = await _unitOfWork.Phims.GetByIdAsync(dto.PhimId, ct);
            if (phim == null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Phim Id={PhimId} khi tạo TapPhim", dto.PhimId);
                throw new PhimNotFoundException(dto.PhimId);
            }

            if (dto.LinkChinh != null)
            {
                var fileNameWithoutExt = $"{SlugHelper.GenerateSlug(phim.TenP)}-{SlugHelper.GenerateSlug(dto.TenTap)}";

                // folder vật lý
                var physicalFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", "episodes");

                // SaveVideoAsync thường trả về filename: xxx.mp4
                var fileName = await FileHelper.SaveVideoAsync(dto.LinkChinh, physicalFolder, fileNameWithoutExt);

                // lưu DB dạng đường dẫn tương đối
                videoPath = Path.Combine("videos", "episodes", fileName).Replace("\\", "/");
            }

            var entity = _factory.Create(dto, videoPath);

            // Lấy danh sách tập phim hiện có của phim này để check duplicate
            var existing = await _unitOfWork.TapPhims.GetByPhimIdAsync(dto.PhimId, ct);

            _domainService.EnsureCanCreate(entity, existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

            var result = await _unitOfWork.TapPhims.CreateAsync(entity, ct);
            if (result is null)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError("ApplicationService: Tạo TapPhim thất bại cho PhimId={PhimId}", dto.PhimId);
                throw new CreateTapPhimFailedException(dto.TenTap);
            }

            await transaction.CommitAsync(ct);

            _logger.LogInformation("ApplicationService: Tạo TapPhim {TenTap} thành công cho PhimId={PhimId}", dto.TenTap, dto.PhimId);

            return ResultDTO.Success($"Tạo tập phim {dto.TenTap} thành công");
        }

        // Update
        public async Task<ResultDTO<UpdateTapPhimResponseDTO>> UpdateAsync(TapPhimUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu Update TapPhim Id={TapId}", dto.TapId);

            var existingEntity = await _unitOfWork.TapPhims.GetByIdAsync(dto.TapId, ct);
            if (existingEntity is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy TapPhim Id={TapId}", dto.TapId);
                throw new TapPhimNotFoundException(dto.TapId);
            }

            string videoPath = existingEntity.LinkChinh ?? string.Empty;

            if (dto.LinkChinh != null)
            {
                // Xóa file cũ nếu có (tương thích cả dữ liệu cũ chỉ lưu filename)
                if (!string.IsNullOrWhiteSpace(existingEntity.LinkChinh))
                {
                    var oldRel = existingEntity.LinkChinh.Contains("/")
                        ? existingEntity.LinkChinh
                        : Path.Combine("videos", "episodes", existingEntity.LinkChinh).Replace("\\", "/");

                    var oldPhysical = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        oldRel.Replace("/", "\\")
                    );

                    FileHelper.DeleteVideo(oldPhysical);
                }

                // Lấy phim để đặt lại tên file
                var phim = await _unitOfWork.Phims.GetByIdAsync(dto.PhimId, ct);
                if (phim == null)
                {
                    _logger.LogWarning("ApplicationService: Không tìm thấy Phim Id={PhimId} khi update TapPhim", dto.PhimId);
                    throw new PhimNotFoundException(dto.PhimId);
                }

                var fileNameWithoutExt = $"{SlugHelper.GenerateSlug(phim.TenP)}-{SlugHelper.GenerateSlug(dto.TenTap)}";

                // folder vật lý
                var physicalFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", "episodes");

                // SaveVideoAsync trả về filename: xxx.mp4
                var fileName = await FileHelper.SaveVideoAsync(dto.LinkChinh, physicalFolder, fileNameWithoutExt);

                // Lưu DB dạng đường dẫn tương đối
                videoPath = Path.Combine("videos", "episodes", fileName).Replace("\\", "/");
            }
            var updated = _factory.Update(dto, videoPath);

            _domainService.EnsureCanUpdate(updated, existingEntity);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

            var result = await _unitOfWork.TapPhims.UpdateAsync(updated, ct);
            if (result is null)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError("ApplicationService: Cập nhật TapPhim Id={TapId} thất bại", dto.TapId);
                throw new UpdateTapPhimFailedException(dto.TenTap);
            }

            await transaction.CommitAsync(ct);

            _logger.LogInformation("ApplicationService: Cập nhật TapPhim Id={TapId} thành công", dto.TapId);

            var response = _mapper.ToUpdateDto(updated);

            return ResultDTO<UpdateTapPhimResponseDTO>.Success(response, $"Cập nhật tập phim {dto.TenTap} thành công");
        }

        // Delete
        public async Task<ResultDTO> DeleteAsync(TapPhimDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu Delete TapPhim Id={TapId}", dto.TapId);

            var entity = await _unitOfWork.TapPhims.GetByIdAsync(dto.TapId, ct);
            if (entity is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy TapPhim Id={TapId}", dto.TapId);
                throw new TapPhimNotFoundException(dto.TapId);
            }

            // Xóa video vật lý
            FileHelper.DeleteVideo(Path.Combine("wwwroot", "videos", entity.LinkChinh));

            await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

            await _unitOfWork.TapPhims.DeleteAsync(entity, ct);

            await transaction.CommitAsync(ct);

            _logger.LogInformation("ApplicationService: Xoá TapPhim Id={TapId} thành công", dto.TapId);

            return ResultDTO.Success($"Xoá tập phim {entity.TenTap} thành công");
        }

        public async Task<ResultListDTO<List<TapPhimResponseDTO>>> SearchAsync(string keyword, int page, int limit, CancellationToken ct = default)
        {
            _logger.LogInformation("Search TapPhim keyword={Keyword}", keyword);

            var (list, total, last) =
                await _unitOfWork.TapPhims.SearchTapPhimAsync(keyword, page, limit, ct);

            if (total == 0)
                throw new TapPhimNotFoundException();

            var dtos = _mapper.ToDtoList(list);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<TapPhimResponseDTO>>
                .Success(dtos, "Tìm kiếm tập phim thành công", pagination);
        }

    }
}
