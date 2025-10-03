using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IDaoDienService
    {
        Task<ResultListDTO<List<DaoDienResponseDTO>>> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO<DaoDienResponseDTO>> GetDetailAsync(int daoDienId, CancellationToken ct = default);
        Task<ResultDTO> CreateDaoDienAsync(DaoDienCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateDaoDienResponseDTO>> UpdateDaoDienAsync(DaoDienUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteDaoDienAsync(DaoDienDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
