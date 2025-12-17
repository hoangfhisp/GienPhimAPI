using GienPhim_Services.WalletService.Application.DTOs.Common;

namespace GienPhim_Services.WalletService.Application.Interfaces.Mappers
{
    public interface IPaginationMapper
    {
        PaginationDTO ToDto(int page, int limit, int total, int last);
    }
}
