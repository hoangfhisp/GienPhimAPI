using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class DienVienResponseMapper : IDienVienResponseMapper
    {
        public DienVienResponseDTO toDto(DienVien dienVien)
        {
            return new DienVienResponseDTO
            {
                DienVienId = dienVien.DienVienId,
                TenDV = dienVien.TenDV,
                MoTaDV = dienVien.MoTaDV
            };
        }

        public List<DienVienResponseDTO> toDtoList(List<DienVien> dienViens)
        {
            return dienViens.Select(dv => toDto(dv)).ToList();
        }

        public UpdateDienVienResponseDTO ToUpdateDto(DienVien dienVien)
        {
            return new UpdateDienVienResponseDTO
            {
                DienVienId = dienVien.DienVienId,
                TenDV = dienVien.TenDV,
                MoTaDV = dienVien.MoTaDV,
                ThoiGianTao = dienVien.ThoiGianTao,
                ThoiGianCapNhat = dienVien.ThoiGianCapNhat
            };
        }

        public List<UpdateDienVienResponseDTO> ToUpdateDtoList(List<DienVien> dienViens)
        {
            return dienViens.Select(dv => ToUpdateDto(dv)).ToList();
        }
    }
}
