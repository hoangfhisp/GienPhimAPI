using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class FilmViewService : IFilmViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilmViewResponseMapper _mapper;
        private readonly IFilmViewDomainService _domainService;
        private readonly ILogger<FilmViewService> _logger;

        public FilmViewService(
            IUnitOfWork unitOfWork,
            IFilmViewResponseMapper mapper,
            IFilmViewDomainService domainService,
            ILogger<FilmViewService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _domainService = domainService;
            _logger = logger;
        }

        // ---------------------- CREATE VIEW ----------------------
        public async Task<ResultDTO> CreateAsync(FilmViewCreateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu Create FilmView cho PhimId={PhimId}, UserId={UserId}", dto.PhimId, dto.UserId);

            // 1. Lấy phim
            var phim = await _unitOfWork.Phims.GetByIdAsync(dto.PhimId, ct);
            if (phim == null)
                throw new PhimNotFoundException(dto.PhimId);

            // 2. Kiểm tra lượt xem gần nhất
            var lastView = await _unitOfWork.FilmViews.GetLastViewByUserAsync(dto.UserId, dto.PhimId, ct);

            if (lastView != null)
            {
                int phimDuration = ParseThoiLuongToMinutes(phim.ThuoiLuongP);
                if ((DateTime.UtcNow - lastView.ThoiGianXem).TotalMinutes < phimDuration)
                {
                    _logger.LogWarning("User {UserId} xem lại phim {PhimId} quá sớm -> không cộng lượt", dto.UserId, dto.PhimId);
                    return ResultDTO.Fail("Bạn đã xem phim này gần đây, chưa tính thêm lượt xem mới.");
                }
            }

            // 3. Tạo FilmView mới
            var filmView = new FilmView
            {
                FilmViewId = Guid.NewGuid().ToString("N"),
                PhimId = dto.PhimId,
                UserId = dto.UserId,
                ThoiGianXem = DateTime.UtcNow
            };

            _domainService.EnsureCanCreate(filmView);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

            var created = await _unitOfWork.FilmViews.CreateAsync(filmView, ct);
            if (created == null)
            {
                await transaction.RollbackAsync(ct);
                throw new CreateFilmViewFailedException(filmView.FilmViewId);
            }

            // 4. Tăng lượt xem phim
            phim.LuotXemP++;
            await _unitOfWork.Phims.UpdateAsync(phim, ct);

            await transaction.CommitAsync(ct);

            _logger.LogInformation("ApplicationService: Tạo FilmView thành công cho PhimId={PhimId}, UserId={UserId}", dto.PhimId, dto.UserId);

            return ResultDTO.Success($"Lượt xem cho phim {phim.TenP} được cộng thành công.");
        }

        // ---------------------- UPDATE ----------------------
        public async Task<ResultDTO<UpdateFilmViewResponseDTO>> UpdateAsync(FilmViewUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu Update FilmView Id={FilmViewId}", dto.FilmViewId);

            var existing = await _unitOfWork.FilmViews.GetByIdAsync(dto.FilmViewId, ct);
            if (existing == null)
                throw new FilmViewNotFoundException(dto.FilmViewId);

            existing.ThoiGianXem = DateTime.UtcNow;

            _domainService.EnsureCanUpdate(existing, existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

            var updated = await _unitOfWork.FilmViews.UpdateAsync(existing, ct);
            if (updated == null)
            {
                await transaction.RollbackAsync(ct);
                throw new UpdateFilmViewFailedException(dto.FilmViewId);
            }

            await transaction.CommitAsync(ct);

            var response = _mapper.ToUpdateDto(updated);

            return ResultDTO<UpdateFilmViewResponseDTO>.Success(response, $"Cập nhật lượt xem thành công.");
        }

        // ---------------------- DELETE ----------------------
        public async Task<ResultDTO> DeleteAsync(FilmViewDeleteDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu Delete FilmView Id={FilmViewId}", dto.FilmViewId);

            var existing = await _unitOfWork.FilmViews.GetByIdAsync(dto.FilmViewId, ct);
            if (existing == null)
                throw new FilmViewNotFoundException(dto.FilmViewId);

            _domainService.EnsureCanDelete(existing);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

            await _unitOfWork.FilmViews.DeleteAsync(existing, ct);

            await transaction.CommitAsync(ct);

            return ResultDTO.Success($"Xoá FilmView Id={dto.FilmViewId} thành công.");
        }

        // ---------------------- HELPER ----------------------
        private int ParseThoiLuongToMinutes(string thuoiLuong)
        {
            if (string.IsNullOrWhiteSpace(thuoiLuong)) return 0;

            thuoiLuong = thuoiLuong.ToLower();

            if (thuoiLuong.EndsWith("p"))
                return int.Parse(thuoiLuong.Replace("p", "").Trim());

            if (thuoiLuong.Contains("h"))
            {
                var parts = thuoiLuong.Split('h');
                int hours = int.Parse(parts[0].Trim());
                int minutes = parts.Length > 1 && parts[1].Contains("m")
                    ? int.Parse(parts[1].Replace("m", "").Trim())
                    : 0;
                return hours * 60 + minutes;
            }

            return int.TryParse(thuoiLuong, out var result) ? result : 0;
        }
    }
}
