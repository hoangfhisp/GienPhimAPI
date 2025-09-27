using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class TheLoaiResponseMapper : ITheLoaiResponseMapper
    {
        // Chuyển đổi từ TheLoai entity sang TheLoaiResponseDTO
        public TheLoaiResponseDTO toDto(TheLoai theLoai)
        {
            return new TheLoaiResponseDTO
            {
                TheLoaiId = theLoai.TheLoaiId,
                TenTL = theLoai.TenTL,
                MoTa = theLoai.MoTa
            };
        }

        // Chuyển đổi từ danh sách TheLoai entity sang danh sách TheLoaiResponseDTO
        public List<TheLoaiResponseDTO> toDtoList(List<TheLoai> theLoais)
        {
            return theLoais.Select(tl => toDto(tl)).ToList();
        }

        // Chuyển đổi từ TheLoai entity sang UpdateTheLoaiResponseDTO
        public UpdateTheLoaiResponseDTO toUpdateDto(TheLoai theLoai)
        {
            return new UpdateTheLoaiResponseDTO
            {
                TheLoaiId = theLoai.TheLoaiId,
                TenTL = theLoai.TenTL,
                MoTa = theLoai.MoTa,
                ThoiGianTao = theLoai.ThoiGianTao,
                ThoiGianCapNhat = theLoai.ThoiGianCapNhat
            };
        }

        public List<UpdateTheLoaiResponseDTO> toUpdateDtoList(List<TheLoai> theLoais)
        {
            return theLoais.Select(tl => toUpdateDto(tl)).ToList();
        }
    }
}
