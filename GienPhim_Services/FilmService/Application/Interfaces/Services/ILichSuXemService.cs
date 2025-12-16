using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface ILichSuXemService
    {
        Task<ResultListDTO<List<LichSuXemResponseDTO>>> GetByUserIdAsync(GetLichSuXemUserRequestDTO request, CancellationToken cancellationToken = default); // Lấy lịch sử xem theo UserId với phân trang
        Task<ResultDTO> XuLyLichSuXemAsync(LichSuXemCreateDTO createDTO, CancellationToken cancellationToken = default); // Xử lý lịch sử xem
    }
}
