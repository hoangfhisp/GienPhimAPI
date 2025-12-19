using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IPhanLoaiTheLoaiOrchestrator
    {
        Task<PhanLoaiTheLoai> CreateAsync(PhanLoaiTheLoaiCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheLoai> UpdateAsync(PhanLoaiTheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(PhanLoaiTheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
