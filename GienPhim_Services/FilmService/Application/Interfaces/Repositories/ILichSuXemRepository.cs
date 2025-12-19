using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface ILichSuXemRepository
    {
        Task<(List<LichSuXem> lichSuXems, int total, int last)> GetUserLichSuXem(string UserId, int page, int limit, CancellationToken cancellationToken = default); // Lấy lịch sử xem của người dùng
        Task<LichSuXem> AddLichSuXemAsync(LichSuXem lichSuXem); // Thêm lịch sử xem
        Task<LichSuXem> UpdateLichSuXemAsync(LichSuXem lichSuXem); // Cập nhật lịch sử xem
        Task<LichSuXem> CheckLichSuXemAsync(int IdPhim, string UserId); // Kiểm tra lịch sử xem
    }
}
