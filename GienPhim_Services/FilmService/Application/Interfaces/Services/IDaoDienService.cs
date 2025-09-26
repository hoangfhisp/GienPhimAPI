using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IDaoDienService
    {
        Task<ResultDTO> CreateDaoDienAsync(DaoDienCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateDaoDienResponseDTO>> UpdateDaoDienAsync(DaoDienUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteDaoDienAsync(DaoDienDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
