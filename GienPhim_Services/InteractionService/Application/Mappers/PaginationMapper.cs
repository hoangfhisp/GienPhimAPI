using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;

namespace GienPhim_Services.InteractionService.Application.Mappers
{
    public class PaginationMapper : IPaginationMapper
    {
        public PaginationDTO ToDto(int page, int limit, int total,int last)
        {
            return new PaginationDTO
            {
                Page = page,
                Limit = limit,
                Last = last,
                Total = total
            };
        }
    }
}
