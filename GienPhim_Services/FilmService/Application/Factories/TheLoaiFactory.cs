using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class TheLoaiFactory : ITheLoaiFactory
    {
        public TheLoai CreateTheLoai(TheLoaiCreateDTO dto)
        {
            return new TheLoai
            {
                TenTL = dto.TenTL?.Trim() ?? string.Empty,
                MoTa = dto.MoTa?.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public TheLoai UpdateTheLoai(TheLoaiUpdateDTO dto)
        {
            var now = DateTime.UtcNow;

            return new TheLoai
            {
                TheLoaiId = dto.TheLoaiId,
                TenTL = (dto.TenTL ?? string.Empty).Trim(),
                MoTa = string.IsNullOrWhiteSpace(dto.MoTa) ? null : dto.MoTa.Trim(),
                ThoiGianCapNhat = now
            };
        }

        public TheLoai DeleteTheLoai(TheLoaiDeleteDTO dto)
        {
            return new TheLoai
            {
                TheLoaiId = dto.TheLoaiId
            };
        }
    }
}
