using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface ITheLoaiFactory
    {
        TheLoai CreateTheLoai(TheLoaiCreateDTO dto);
        TheLoai UpdateTheLoai(TheLoaiUpdateDTO dto);
        TheLoai DeleteTheLoai(TheLoaiDeleteDTO dto);
    }
}
