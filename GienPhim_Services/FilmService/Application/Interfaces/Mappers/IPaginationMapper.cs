using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IPaginationMapper
    {
        public PaginationDTO ToDto(int page, int limit, int total, int last);
    }
}
