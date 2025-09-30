using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class QuocGiaFactory : IQuocGiaFactory
    {
        public QuocGia CreateQuocGia(QuocGiaCreateDTO dto)
        {
            return new QuocGia
            {
                TenQG = dto.TenQG?.Trim() ?? string.Empty,
                MoTaQG = dto.MoTaQG?.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public QuocGia UpdateQuocGia(QuocGiaUpdateDTO dto)
        {
            var now = DateTime.UtcNow;

            return new QuocGia
            {
                QuocGiaId = dto.QuocGiaId,
                TenQG = (dto.TenQG ?? string.Empty).Trim(),
                MoTaQG = string.IsNullOrWhiteSpace(dto.MoTaQG) ? null : dto.MoTaQG.Trim(),
                ThoiGianCapNhat = now
            };
        }

        public QuocGia DeleteQuocGia(QuocGiaDeleteDTO dto)
        {
            return new QuocGia
            {
                QuocGiaId = dto.QuocGiaId
            };
        }
    }
}
