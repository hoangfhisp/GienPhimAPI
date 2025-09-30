using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class DaoDienResponseMapper : IDaoDienResponseMapper
    {
        // Chuyển đổi từ DaoDien entity sang DaoDienResponseDTO
        public DaoDienResponseDTO toDto (DaoDien daoDien)
        {
            return new DaoDienResponseDTO
            {
                DaoDienId = daoDien.DaoDienId,
                TenDD = daoDien.TenDD,
                MoTaDD = daoDien.MoTaDD
            };
        }

        // Chuyển đổi từ danh sách DaoDien entity sang danh sách DaoDienResponseDTO
        public List<DaoDienResponseDTO> toDtoList(List<DaoDien> daoDiens)
        {
            return daoDiens.Select(dd => toDto(dd)).ToList();
        }

        public UpdateDaoDienResponseDTO ToUpdateDto(DaoDien daoDien)
        {
            return new UpdateDaoDienResponseDTO
            {
                DaoDienId = daoDien.DaoDienId,
                TenDD = daoDien.TenDD,
                MoTaDD = daoDien.MoTaDD,
                ThoiGianTao = daoDien.ThoiGianTao,
                ThoiGianCapNhat = daoDien.ThoiGianCapNhat
            };
        }

        public List<UpdateDaoDienResponseDTO> ToUpdateDtoList(List<DaoDien> daoDiens)
        {
            return daoDiens.Select(dd => ToUpdateDto(dd)).ToList();
        }
    }
}
