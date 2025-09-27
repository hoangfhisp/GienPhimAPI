using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class DienVienFactory : IDienVienFactory
    {
        public DienVien CreateDienVien(DienVienCreateDTO dto)
        {
            return new DienVien
            {
                TenDV = dto.TenDV?.Trim() ?? string.Empty,
                MoTaDV = dto.MoTaDV?.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public DienVien UpdateDienVien(DienVienUpdateDTO dto)
        {
            var now = DateTime.UtcNow;

            return new DienVien
            {
                DienVienId = dto.DienVienId,
                TenDV = (dto.TenDV ?? string.Empty).Trim(),
                MoTaDV = string.IsNullOrWhiteSpace(dto.MoTaDV) ? null : dto.MoTaDV.Trim(),
                ThoiGianCapNhat = now
            };
        }

        public DienVien DeleteDienVien(DienVienDeleteDTO dto)
        {
            return new DienVien
            {
                DienVienId = dto.DienVienId
            };
        }
    }
}
