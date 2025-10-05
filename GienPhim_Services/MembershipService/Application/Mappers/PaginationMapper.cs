using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;

namespace GienPhim_Services.MembershipService.Application.Mappers
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
