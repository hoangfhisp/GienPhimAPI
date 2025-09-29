using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IDienVienOrchestrator
    {
        Task<DienVien> CreateDienVienAsync(DienVienCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<DienVien> UpdateDienVienAsync(DienVienUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteDienVienAsync(DienVienDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
