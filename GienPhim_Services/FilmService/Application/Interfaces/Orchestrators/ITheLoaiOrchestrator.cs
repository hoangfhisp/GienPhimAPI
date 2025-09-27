using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface ITheLoaiOrchestrator
    {
        Task<IEnumerable<TheLoai>> GetAllTheLoaisAsync(CancellationToken cancellationToken = default);
        Task<TheLoai> CreateTheLoaiAsync(TheLoaiCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<TheLoai> UpdateTheLoaiAsync(TheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteTheLoaiAsync(TheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
