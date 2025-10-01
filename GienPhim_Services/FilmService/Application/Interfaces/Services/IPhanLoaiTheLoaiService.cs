using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IPhanLoaiTheLoaiService
    {
        Task<ResultListDTO<List<PhanLoaiTheLoaiResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateAsync(PhanLoaiTheLoaiCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdatePhanLoaiTheLoaiResponseDTO>> UpdateAsync(PhanLoaiTheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteAsync(PhanLoaiTheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
