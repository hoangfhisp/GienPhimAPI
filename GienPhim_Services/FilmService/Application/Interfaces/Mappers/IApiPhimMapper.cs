using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Mappers
{
    public interface IApiPhimMapper
    {
        // Ánh xạ phim chính
        Phim ToPhimEntity(ApiPhimDTO dto, NgonNgu ngonNgu, QuocGia quocGia, string slug);

        // Ngôn ngữ & quốc gia
        NgonNgu ToNgonNguEntity(ApiPhimDTO dto);
        QuocGia ToQuocGiaEntity(ApiPhimDTO dto);

        // Thể loại & phân loại
        List<TheLoai> ToTheLoaiEntities(ApiPhimDTO dto);
        List<PhanLoaiTheLoai> ToPhanLoaiTheLoaiEntities(int phimId, IEnumerable<TheLoai> theLoais);

        // Diễn viên & diễn viên của phim
        List<DienVien> ToDienVienEntities(ApiPhimDTO dto);
        List<DienVienCuaPhim> ToDienVienCuaPhimEntities(int phimId, IEnumerable<DienVien> dienViens);

        // Đạo diễn & đạo diễn của phim
        List<DaoDien> ToDaoDienEntities(ApiPhimDTO dto);
        List<DaoDienCuaPhim> ToDaoDienCuaPhimEntities(int phimId, IEnumerable<DaoDien> daoDiens);

        // Tập phim
        List<TapPhim> ToTapPhimEntities(ApiPhimDTO dto, int phimId);
    }
}
