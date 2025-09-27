using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Orchestrators
{
    public interface IQuocGiaOrchestrator
    {
        Task<IEnumerable<QuocGia>> GetAllQuocGiasAsync(CancellationToken cancellationToken = default);
        Task<QuocGia> CreateQuocGiaAsync(QuocGiaCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<QuocGia> UpdateQuocGiaAsync(QuocGiaUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteQuocGiaAsync(QuocGiaDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
