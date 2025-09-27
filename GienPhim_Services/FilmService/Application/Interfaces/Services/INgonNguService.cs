using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface INgonNguService
    {
        Task<ResultListDTO<List<NgonNguResponseDTO>>> GetAllNgonNgusAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateNgonNguAsync(NgonNguCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateNgonNguResponseDTO>> UpdateNgonNguAsync(NgonNguUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteNgonNguAsync(NgonNguDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
