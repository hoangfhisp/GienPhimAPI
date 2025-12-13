using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface ITheTagRepository
    {
        Task<(List<TheTag> theTags, int total, int last)> GetAllTheTagsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<TheTag?> GetByIdAsync(int theTagId, CancellationToken ct = default);
        Task<TheTag> CreateAsync(TheTag theTag, CancellationToken ct = default);
        Task<TheTag> UpdateAsync(TheTag theTag, CancellationToken ct = default);
        Task DeleteAsync(TheTag theTag, CancellationToken ct = default);
        Task<List<TheTag>> SearchTheTagsAsync(string keyword, CancellationToken cancellationToken = default);
    }
}
