using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IQuocGiaResponseMapper
    {
        QuocGiaResponseDTO ToDto(QuocGia quocGia);
        List<QuocGiaResponseDTO> ToDtoList(List<QuocGia> quocGias);
        UpdateQuocGiaResponseDTO ToUpdateDto(QuocGia quocGia);
        List<UpdateQuocGiaResponseDTO> ToUpdateDtoList(List<QuocGia> quocGias);
    }
}
