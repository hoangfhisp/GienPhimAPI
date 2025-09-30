using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IDienVienService
    {
        Task<ResultListDTO<List<DienVienResponseDTO>>> GetAllDienViensAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateDienVienAsync(DienVienCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateDienVienResponseDTO>> UpdateDienVienAsync(DienVienUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteDienVienAsync(DienVienDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
