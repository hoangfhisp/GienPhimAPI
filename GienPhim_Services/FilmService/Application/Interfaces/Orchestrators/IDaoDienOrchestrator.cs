using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IDaoDienOrchestrator
    {
        Task<DaoDien> CreateDaoDienAsync(DaoDienCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<DaoDien> UpdateDaoDienAsync(DaoDienUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteDaoDienAsync(DaoDienDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
