using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface ITheTagFactory
    {
        TheTag CreateTheTag(TheTagCreateDTO dto);
        TheTag UpdateTheTag(TheTagUpdateDTO dto);
        TheTag DeleteTheTag(TheTagDeleteDTO dto);
    }
}
