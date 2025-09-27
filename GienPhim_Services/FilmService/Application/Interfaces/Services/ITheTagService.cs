using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.Interfaces.Services
{
    public interface ITheTagService
    {
        Task<ResultListDTO<List<TheTagResponseDTO>>> GetAllTheTagsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateTheTagAsync(TheTagCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateTheTagResponseDTO>> UpdateTheTagAsync(TheTagUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteTheTagAsync(TheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
