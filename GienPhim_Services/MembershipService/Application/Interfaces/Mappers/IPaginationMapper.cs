using GienPhim_Services.MembershipService.Application.DTOs.Common;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Mappers
{
    public interface IPaginationMapper
    {
        public PaginationDTO ToDto(int page, int limit, int total, int last);
    }
}
