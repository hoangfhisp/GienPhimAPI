using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;

namespace GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces
{
    public interface IApiPhimServiceClient
    {
        Task<ApiPhimDTO> GetPhimDataAsync(string slug, CancellationToken ct = default);
    }
}
