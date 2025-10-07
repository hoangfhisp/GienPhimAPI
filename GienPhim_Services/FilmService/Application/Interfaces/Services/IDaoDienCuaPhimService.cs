using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IDaoDienCuaPhimService
    {
        Task<ResultListDTO<List<DaoDienCuaPhimResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateAsync(DaoDienCuaPhimCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateDaoDienCuaPhimResponseDTO>> UpdateAsync(DaoDienCuaPhimUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteAsync(DaoDienCuaPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
