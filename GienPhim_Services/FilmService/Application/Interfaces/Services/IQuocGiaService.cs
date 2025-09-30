using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface IQuocGiaService
    {
        Task<ResultListDTO<List<QuocGiaResponseDTO>>> GetAllQuocGiasAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateQuocGiaAsync(QuocGiaCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateQuocGiaResponseDTO>> UpdateQuocGiaAsync(QuocGiaUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteQuocGiaAsync(QuocGiaDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
