using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface ITapPhimService
    {
        Task<ResultListDTO<List<TapPhimResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultListDTO<List<TapPhimResponseDTO>>> GetByPhimIdAsync(int phimId, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateAsync(TapPhimCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateTapPhimResponseDTO>> UpdateAsync(TapPhimUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteAsync(TapPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default);
        Task<ResultListDTO<List<TapPhimResponseDTO>>> SearchAsync(string keyword, int page, int limit, CancellationToken ct = default);
    }
}
