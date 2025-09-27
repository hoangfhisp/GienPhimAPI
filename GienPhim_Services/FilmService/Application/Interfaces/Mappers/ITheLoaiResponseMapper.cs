using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface ITheLoaiResponseMapper
    {
        TheLoaiResponseDTO toDto(TheLoai theLoai); // Entity → DTO
        List<TheLoaiResponseDTO> toDtoList(List<TheLoai> theLoais); // List<Entity> → List<DTO>
        UpdateTheLoaiResponseDTO toUpdateDto(TheLoai theLoai); // Entity → Update DTO
        List<UpdateTheLoaiResponseDTO> toUpdateDtoList(List<TheLoai> theLoais);
    }
}
