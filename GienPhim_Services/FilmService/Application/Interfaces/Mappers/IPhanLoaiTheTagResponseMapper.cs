using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IPhanLoaiTheTagResponseMapper
    {
        PhanLoaiTheTagResponseDTO ToDto(PhanLoaiTheTag phanLoaiTheTag);
        List<PhanLoaiTheTagResponseDTO> ToDtoList(List<PhanLoaiTheTag> phanLoaiTheTags);

        UpdatePhanLoaiTheTagResponseDTO ToUpdateDto(PhanLoaiTheTag phanLoaiTheTag);
        List<UpdatePhanLoaiTheTagResponseDTO> ToUpdateDtoList(List<PhanLoaiTheTag> phanLoaiTheTags);
    }
}
