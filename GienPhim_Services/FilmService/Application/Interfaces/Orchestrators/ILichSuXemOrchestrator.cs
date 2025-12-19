using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.DTOs.User;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface ILichSuXemOrchestrator
    {
        Task<LichSuXem> CreateLichSuXem(LichSuXemCreateDTO createDTO, CancellationToken cancellationToken = default); // Tạo lịch sử xem mới
        Task<UserResponseDTO> FetchUserByUserIdAsync(string userId, CancellationToken cancellationToken = default); // Lấy thông tin user từ dịch vụ UserService theo userId
    }
}
