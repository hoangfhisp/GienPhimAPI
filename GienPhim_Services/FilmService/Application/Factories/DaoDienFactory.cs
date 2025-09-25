using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class DaoDienFactory : IDaoDienFactory
    {
        public DaoDien CreateDaoDien(DaoDienCreateDTO dto)
        {
            return new DaoDien
            {
                TenDD = dto.TenDD?.Trim() ?? string.Empty,
                MoTaDD = dto.MoTaDD?.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public void UpdateDaoDien(DaoDien existing, DaoDienUpdateDTO dto)
        {
            existing.TenDD = dto.TenDD?.Trim() ?? string.Empty;
            existing.MoTaDD = dto.MoTaDD?.Trim();
            existing.ThoiGianCapNhat = DateTime.UtcNow;
        }
    }
}
