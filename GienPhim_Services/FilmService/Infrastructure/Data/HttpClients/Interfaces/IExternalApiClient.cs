using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;

namespace GienPhim_Services.FilmService.Application.Interfaces.HttpClients
{
    public interface IExternalApiClient
    {
        Task<ApiPhimListResponse?> GetLatestFilmsV3Async(int page, CancellationToken ct = default);
    }
}
