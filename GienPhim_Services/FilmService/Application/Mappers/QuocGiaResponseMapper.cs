using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class QuocGiaResponseMapper : IQuocGiaResponseMapper
    {
        public QuocGiaResponseDTO ToDto(QuocGia quocGia)
        {
            return new QuocGiaResponseDTO
            {
                QuocGiaId = quocGia.QuocGiaId,
                TenQG = quocGia.TenQG,
                MoTaQG = quocGia.MoTaQG
            };
        }

        public List<QuocGiaResponseDTO> ToDtoList(List<QuocGia> quocGias)
        {
            return quocGias.Select(qg => ToDto(qg)).ToList();
        }

        public UpdateQuocGiaResponseDTO ToUpdateDto(QuocGia quocGia)
        {
            return new UpdateQuocGiaResponseDTO
            {
                QuocGiaId = quocGia.QuocGiaId,
                TenQG = quocGia.TenQG,
                MoTaQG = quocGia.MoTaQG,
                ThoiGianTao = quocGia.ThoiGianTao,
                ThoiGianCapNhat = quocGia.ThoiGianCapNhat
            };
        }

        public List<UpdateQuocGiaResponseDTO> ToUpdateDtoList(List<QuocGia> quocGias)
        {
            return quocGias.Select(qg => ToUpdateDto(qg)).ToList();
        }
    }
}
