using GienPhim_Services.InteractionService.Application.DTOs.Common;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Mappers
{
    public interface IPaginationMapper
    {
        public PaginationDTO ToDto(int page, int limit, int total, int last);
    }
}
