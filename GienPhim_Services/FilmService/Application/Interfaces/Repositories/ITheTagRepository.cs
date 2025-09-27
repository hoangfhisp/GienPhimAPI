using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface ITheTagRepository
    {
        Task<List<TheTag>> GetAllTheTagsAsync(CancellationToken cancellationToken = default);
        Task<TheTag?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TheTag> CreateAsync(TheTag theTag, CancellationToken ct = default);
        Task<TheTag> UpdateAsync(TheTag theTag, CancellationToken ct = default);
        Task DeleteAsync(TheTag theTag, CancellationToken ct = default);
    }
}
