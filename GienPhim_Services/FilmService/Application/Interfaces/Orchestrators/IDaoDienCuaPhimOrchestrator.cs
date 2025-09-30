using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IDaoDienCuaPhimOrchestrator
    {
        Task<DaoDienCuaPhim> CreateAsync(DaoDienCuaPhimCreateDTO createDto, IEnumerable<DaoDienCuaPhim> existingRelations, CancellationToken cancellationToken = default);
        Task<DaoDienCuaPhim> UpdateAsync(DaoDienCuaPhimUpdateDTO updateDto, DaoDienCuaPhim? existingRelation, CancellationToken cancellationToken = default);
        Task DeleteAsync(DaoDienCuaPhimDeleteDTO deleteDto, DaoDienCuaPhim? existingRelation, CancellationToken cancellationToken = default);
    }
}
