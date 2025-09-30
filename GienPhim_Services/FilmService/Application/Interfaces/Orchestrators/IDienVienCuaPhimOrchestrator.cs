using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IDienVienCuaPhimOrchestrator
    {
        Task<DienVienCuaPhim> CreateAsync(DienVienCuaPhimCreateDTO dto, IEnumerable<DienVienCuaPhim> existingRelations, CancellationToken cancellationToken = default);
        Task<DienVienCuaPhim> UpdateAsync(DienVienCuaPhimUpdateDTO dto, DienVienCuaPhim? existingRelation, CancellationToken cancellationToken = default);
        Task DeleteAsync(DienVienCuaPhimDeleteDTO dto, CancellationToken cancellationToken = default);
    }
}
