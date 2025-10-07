using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IDienVienCuaPhimService
    {
        Task<ResultListDTO<List<DienVienCuaPhimResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateAsync(DienVienCuaPhimCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateDienVienCuaPhimResponseDTO>> UpdateAsync(DienVienCuaPhimUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteAsync(DienVienCuaPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
