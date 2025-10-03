using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IPhimService
    {
        Task<ResultListDTO<List<PhimResponseDTO>>> GetAllPhimsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO<PhimResponseDTO>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ResultDTO> CreatePhimAsync(PhimCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdatePhimResponseDTO>> UpdatePhimAsync(PhimUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeletePhimAsync(PhimDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
