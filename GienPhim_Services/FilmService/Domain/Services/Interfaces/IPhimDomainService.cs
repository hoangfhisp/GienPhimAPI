using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Domain.Services.Interfaces
{
    public interface IPhimDomainService
    {
        void EnsureCanCreate(Phim phim, string avatar, string poster);
        void EnsureCanUpdate(Phim phim, string? avatar, string? poster);
        void EnsureCanDelete(Phim phim);
        void ValidateAvatar(string avatar);
        void ValidatePoster(string poster);

    }
}
