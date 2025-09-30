
using GienPhim_Services.UserService.Application.Interfaces.Mappers;
using GienPhim_Services.UserService.Application.DTOs.Common;

namespace GienPhim_Services.UserService.Application.Mappers
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
