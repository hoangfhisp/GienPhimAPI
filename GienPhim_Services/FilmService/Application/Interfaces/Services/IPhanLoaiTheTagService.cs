using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IPhanLoaiTheTagService
    {
        Task<ResultListDTO<List<PhanLoaiTheTagResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateAsync(PhanLoaiTheTagCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdatePhanLoaiTheTagResponseDTO>> UpdateAsync(PhanLoaiTheTagUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteAsync(PhanLoaiTheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
