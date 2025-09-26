using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IDaoDienFactory
    {
        DaoDien CreateDaoDien(DaoDienCreateDTO dto);
        DaoDien UpdateDaoDien(DaoDienUpdateDTO dto);
        DaoDien DeleteDaoDien(DaoDienDeleteDTO dto);
    }
}
