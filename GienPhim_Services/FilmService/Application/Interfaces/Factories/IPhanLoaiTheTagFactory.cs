using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IPhanLoaiTheTagFactory
    {
        PhanLoaiTheTag Create(PhanLoaiTheTagCreateDTO dto);
        PhanLoaiTheTag Update(PhanLoaiTheTagUpdateDTO dto);
        PhanLoaiTheTag Delete(PhanLoaiTheTagDeleteDTO dto);
    }
}
