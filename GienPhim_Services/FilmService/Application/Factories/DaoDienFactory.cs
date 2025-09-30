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

        public DaoDien UpdateDaoDien(DaoDienUpdateDTO dto)
        {
            var now = DateTime.UtcNow;

            return new DaoDien
            {
                DaoDienId = dto.DaoDienId,
                TenDD = (dto.TenDD ?? string.Empty).Trim(),
                MoTaDD = string.IsNullOrWhiteSpace(dto.MoTaDD) ? null : dto.MoTaDD.Trim(),
                ThoiGianCapNhat = now
            };
        }

        public DaoDien DeleteDaoDien(DaoDienDeleteDTO dto)
        {
            return new DaoDien
            {
                DaoDienId = dto.DaoDienId
            };
        }
    }
}
