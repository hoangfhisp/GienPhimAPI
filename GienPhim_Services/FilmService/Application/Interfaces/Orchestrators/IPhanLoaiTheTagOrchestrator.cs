using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IPhanLoaiTheTagOrchestrator
    {
        Task<PhanLoaiTheTag> CreateAsync(PhanLoaiTheTagCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<PhanLoaiTheTag> UpdateAsync(PhanLoaiTheTagUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(PhanLoaiTheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
