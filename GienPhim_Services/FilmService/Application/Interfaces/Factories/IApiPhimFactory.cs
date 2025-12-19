using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IApiPhimFactory
    {
        Phim CreatePhim(ApiPhimDTO dto, NgonNgu ngonNgu, QuocGia quocGia, string slug);
        NgonNgu CreateNgonNgu(ApiPhimDTO dto);
        QuocGia CreateQuocGia(ApiPhimDTO dto);
        List<TheLoai> CreateTheLoais(ApiPhimDTO dto);
        List<DienVien> CreateDienViens(ApiPhimDTO dto);
        List<DaoDien> CreateDaoDiens(ApiPhimDTO dto);
        List<TapPhim> CreateTapPhims(ApiPhimDTO dto, int phimId);
        void UpdatePhim(Phim existing, ApiPhimDTO dto);
        void UpdateTapPhims(Phim existing, ApiPhimDTO dto);
        Task<Phim?> GetExistingPhimAsync(string slug, CancellationToken ct = default);
    }
}
