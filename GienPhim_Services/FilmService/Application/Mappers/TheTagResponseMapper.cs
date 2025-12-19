using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class TheTagResponseMapper : ITheTagResponseMapper
    {
        public TheTagResponseDTO toDto(TheTag theTag)
        {
            return new TheTagResponseDTO
            {
                TheTagId = theTag.TheTagId,
                TenT = theTag.TenT,
                MoTaT = theTag.MoTaT
            };
        }

        public List<TheTagResponseDTO> toDtoList(List<TheTag> theTags)
        {
            return theTags.Select(t => toDto(t)).ToList();
        }

        public UpdateTheTagResponseDTO ToUpdateDto(TheTag theTag)
        {
            return new UpdateTheTagResponseDTO
            {
                TheTagId = theTag.TheTagId,
                TenT = theTag.TenT,
                MoTaT = theTag.MoTaT,
                ThoiGianTao = theTag.ThoiGianTao,
                ThoiGianCapNhat = theTag.ThoiGianCapNhat
            };
        }

        public List<UpdateTheTagResponseDTO> ToUpdateDtoList(List<TheTag> theTags)
        {
            return theTags.Select(t => ToUpdateDto(t)).ToList();
        }
    }
}
