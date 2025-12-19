using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IPhanLoaiTheLoaiResponseMapper
    {
        PhanLoaiTheLoaiResponseDTO ToDto(PhanLoaiTheLoai phanLoaiTheLoai);
        List<PhanLoaiTheLoaiResponseDTO> ToDtoList(List<PhanLoaiTheLoai> phanLoaiTheLoais);

        UpdatePhanLoaiTheLoaiResponseDTO ToUpdateDto(PhanLoaiTheLoai phanLoaiTheLoai);
        List<UpdatePhanLoaiTheLoaiResponseDTO> ToUpdateDtoList(List<PhanLoaiTheLoai> phanLoaiTheLoais);
    }
}
