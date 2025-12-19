
using GienPhim_Services.WalletService.Application.Interfaces.Mappers;
using GienPhim_Services.WalletService.Application.DTOs.Common;

namespace GienPhim_Services.WalletService.Application.Mappers
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
