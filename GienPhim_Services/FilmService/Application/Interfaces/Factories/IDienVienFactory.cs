using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IDienVienFactory
    {
        DienVien CreateDienVien(DienVienCreateDTO dto);
        DienVien UpdateDienVien(DienVienUpdateDTO dto);
        DienVien DeleteDienVien(DienVienDeleteDTO dto);
    }
}
