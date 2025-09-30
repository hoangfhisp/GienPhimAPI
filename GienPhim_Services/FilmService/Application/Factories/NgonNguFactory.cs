using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class NgonNguFactory : INgonNguFactory
    {
        public NgonNgu CreateNgonNgu(NgonNguCreateDTO dto)
        {
            return new NgonNgu
            {
                TenNN = dto.TenNN?.Trim() ?? string.Empty,
                MoTaNN = dto.MoTaNN?.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public NgonNgu UpdateNgonNgu(NgonNguUpdateDTO dto)
        {
            var now = DateTime.UtcNow;

            return new NgonNgu
            {
                NgonNguId = dto.NgonNguId,
                TenNN = (dto.TenNN ?? string.Empty).Trim(),
                MoTaNN = string.IsNullOrWhiteSpace(dto.MoTaNN) ? null : dto.MoTaNN.Trim(),
                ThoiGianCapNhat = now
            };
        }

        public NgonNgu DeleteNgonNgu(NgonNguDeleteDTO dto)
        {
            return new NgonNgu
            {
                NgonNguId = dto.NgonNguId
            };
        }
    }
}
