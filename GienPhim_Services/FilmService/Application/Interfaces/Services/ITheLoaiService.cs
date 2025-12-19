using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface ITheLoaiService
    {
        Task<ResultListDTO<List<TheLoaiResponseDTO>>> GetAllTheLoaisAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO<TheLoaiResponseDTO>> GetDetailAsync(int theLoaiId, CancellationToken ct = default);
        Task<ResultDTO> CreateTheLoaiAsync(TheLoaiCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateTheLoaiResponseDTO>> UpdateTheLoaiAsync(TheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteTheLoaiAsync(TheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default);
        Task<ResultListDTO<List<TheLoaiResponseDTO>>> SearchTheLoaisAsync(string keyword, CancellationToken ct = default);
    }
}
