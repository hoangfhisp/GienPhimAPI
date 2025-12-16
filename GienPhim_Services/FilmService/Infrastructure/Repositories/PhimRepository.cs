using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Quartz.Util;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class PhimRepository : IPhimRepository
    {
        private readonly FilmDbContext _context;

        public PhimRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Phim> phims, int total, int last)> GetAllPhimsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.Phim
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .Include(p => p.PhanLoaiTheLoais)
                    .ThenInclude(pl => pl.TheLoai)
                .Include(p => p.DaoDienCuaPhims)
                    .ThenInclude(ddp => ddp.DaoDien)
                .Include(p => p.DienVienCuaPhims)
                    .ThenInclude(dvcp => dvcp.DienVien)
                .Include(p => p.PhanLoaiTheTags)
                    .ThenInclude(pt => pt.TheTag)
                // .Include(p => p.TapPhims) // nếu muốn lấy luôn số tập để tự đếm
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var phims = await query
                .OrderBy(p => p.PhimId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (phims, total, last);
        }

        public async Task<Phim?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Phim
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .Include(p => p.PhanLoaiTheLoais)
                    .ThenInclude(pl => pl.TheLoai)
                .Include(p => p.DaoDienCuaPhims)
                    .ThenInclude(dp => dp.DaoDien)
                .Include(p => p.DienVienCuaPhims)
                    .ThenInclude(dv => dv.DienVien)
                .Include(p => p.PhanLoaiTheTags)
                    .ThenInclude(pt => pt.TheTag)
                .FirstOrDefaultAsync(p => p.PhimId == id, cancellationToken);
        }

        // Lấy list new phim mới thêm
        public async Task<(List<Phim> phims, int total,int last )>GetNewPhimAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.Phim
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .Include(p => p.PhanLoaiTheLoais)
                    .ThenInclude(pl => pl.TheLoai)
                .Include(p => p.DaoDienCuaPhims)
                    .ThenInclude(dp => dp.DaoDien)
                .Include(p => p.DienVienCuaPhims)
                    .ThenInclude(dv => dv.DienVien)
                .Include(p => p.PhanLoaiTheTags)
                    .ThenInclude(pt => pt.TheTag)
                 .OrderByDescending(p => p.ThoiGianTao)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var phims = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (phims, total, last);
        }

        // Lấy list Top view phim mới thêm
        public async Task<(List<Phim> phims, int total, int last)> GetTopViewPhimAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.Phim
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .Include(p => p.PhanLoaiTheLoais)
                    .ThenInclude(pl => pl.TheLoai)
                .Include(p => p.DaoDienCuaPhims)
                    .ThenInclude(dp => dp.DaoDien)
                .Include(p => p.DienVienCuaPhims)
                    .ThenInclude(dv => dv.DienVien)
                .Include(p => p.PhanLoaiTheTags)
                    .ThenInclude(pt => pt.TheTag)
                 .OrderByDescending(p => p.LuotXemP)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var phims = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (phims, total, last);
        }

        public async Task<Phim?> GetBySlugAsync(string slug, CancellationToken ct = default)
        {
            return await _context.Phim
                .FirstOrDefaultAsync(p => p.NguonP == slug, ct);
        }

        public async Task<Phim> CreateAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            await _context.Phim.AddAsync(phim, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return phim;
        }

        public async Task<Phim> UpdateAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            _context.Phim.Update(phim);
            await _context.SaveChangesAsync(cancellationToken);
            return phim;
        }

        public async Task DeleteAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            _context.Phim.Remove(phim);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Tìm kiếm phim theo từ khóa (gần đúng)
        public async Task<(List<Phim> phims, int total, int last)> SearchPhimsAsync(string keyword,int page, int limit, CancellationToken cancellationToken = default)
        {
            // Chuẩn hóa từ khóa: bỏ dấu, lower, trim
            var key = SlugHelper.NormalizeSearchText(keyword);

            // Lấy tất cả phim (chỉ lấy những field cần)
            var phims = await _context.Phim
                .AsNoTracking()
                .Select(p => new Phim
                {
                    PhimId = p.PhimId,
                    TenP = p.TenP,
                    MoTaP = p.MoTaP,
                    AvatarP = p.AvatarP,
                    AvatarPoster = p.AvatarPoster,
                    NguonP = p.NguonP,
                    ThuoiLuongP = p.ThuoiLuongP,
                    NgonNguId = p.NgonNguId,
                    QuocGiaId = p.QuocGiaId,
                    TenCTSXP = p.TenCTSXP
                })
                .ToListAsync(cancellationToken);


            // So sánh bỏ dấu (ở tầng C#)
            var matched = phims
                .Where(p =>
                    SlugHelper.NormalizeSearchText(p.TenP ?? "").Contains(key) ||
                    SlugHelper.NormalizeSearchText(p.TenCTSXP ?? "").Contains(key) ||
                    SlugHelper.NormalizeSearchText(p.NguonP ?? "").Contains(key)
                )
                .OrderByDescending(p => p.PhimId)
                .ToList();


            var total = matched.Count();
            var last = (int)Math.Ceiling((double)total / limit);

            var result = matched
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return (result,total,last);
        }

        // Lọc phim theo nhiều tiêu chí với phân trang
        public async Task<(List<Phim> phims, int total, int last)> FilterPhimsAsync(string? ngonNguSlug, string? quocGiaSlug, string? theLoaiSlug, string? tenDienVien, string? tenDaoDien, int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.Phim
                .Include(p => p.NgonNgu)
                .Include(p => p.QuocGia)
                .Include(p => p.PhanLoaiTheLoais).ThenInclude(tl => tl.TheLoai)
                .Include(p => p.DienVienCuaPhims).ThenInclude(dv => dv.DienVien)
                .Include(p => p.DaoDienCuaPhims).ThenInclude(dd => dd.DaoDien)
                .AsQueryable();

            // Ngôn ngữ
            if (!string.IsNullOrWhiteSpace(ngonNguSlug))
            {
                query = query.Where(p => p.NgonNgu != null &&
                                         p.NgonNgu.TenNN == ngonNguSlug);
            }

            // Quốc gia
            if (!string.IsNullOrWhiteSpace(quocGiaSlug))
            {
                query = query.Where(p => p.QuocGia != null &&
                                         p.QuocGia.MoTaQG == quocGiaSlug);
            }

            // Thể loại
            if (!string.IsNullOrWhiteSpace(theLoaiSlug))
            {
                query = query.Where(p =>
                    p.PhanLoaiTheLoais.Any(tl => tl.TheLoai.MoTa == theLoaiSlug));
            }

            // Diễn viên
            if (!string.IsNullOrWhiteSpace(tenDienVien))
            {
                query = query.Where(p =>
                    p.DienVienCuaPhims.Any(dv => dv.DienVien.TenDV == tenDienVien));
            }

            // Đạo diễn
            if (!string.IsNullOrWhiteSpace(tenDaoDien))
            {
                query = query.Where(p =>
                    p.DaoDienCuaPhims.Any(dd => dd.DaoDien.TenDD == tenDaoDien));
            }

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var phims = await query
                .OrderByDescending(p => p.PhimId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (phims, total, last);
        }


    }
}
