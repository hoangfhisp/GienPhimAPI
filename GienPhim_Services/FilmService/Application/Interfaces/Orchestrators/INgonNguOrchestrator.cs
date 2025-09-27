using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface INgonNguOrchestrator
    {
        Task<IEnumerable<NgonNgu>> GetAllNgonNgusAsync(CancellationToken cancellationToken = default);
        Task<NgonNgu> CreateNgonNguAsync(NgonNguCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<NgonNgu> UpdateNgonNguAsync(NgonNguUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteNgonNguAsync(NgonNguDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
