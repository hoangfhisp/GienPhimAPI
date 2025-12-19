using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class NgonNguResponseMapper : INgonNguResponseMapper
    {
        public NgonNguResponseDTO ToDto(NgonNgu ngonNgu)
        {
            return new NgonNguResponseDTO
            {
                NgonNguId = ngonNgu.NgonNguId,
                TenNN = ngonNgu.TenNN,
                MoTaNN = ngonNgu.MoTaNN
            };
        }

        public List<NgonNguResponseDTO> ToDtoList(List<NgonNgu> ngonNgus)
        {
            return ngonNgus.Select(nn => ToDto(nn)).ToList();
        }

        public UpdateNgonNguResponseDTO ToUpdateDto(NgonNgu ngonNgu)
        {
            return new UpdateNgonNguResponseDTO
            {
                NgonNguId = ngonNgu.NgonNguId,
                TenNN = ngonNgu.TenNN,
                MoTaNN = ngonNgu.MoTaNN,
                ThoiGianTao = ngonNgu.ThoiGianTao,
                ThoiGianCapNhat = ngonNgu.ThoiGianCapNhat
            };
        }

        public List<UpdateNgonNguResponseDTO> ToUpdateDtoList(List<NgonNgu> ngonNgus)
        {
            return ngonNgus.Select(nn => ToUpdateDto(nn)).ToList();
        }
    }
}
