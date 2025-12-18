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

        public async Task<Phim?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Phim
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

        private T? AttachOrGetTracked<T>(T? entity, Func<T, int> getId) where T : class
        {
            if (entity == null) return null;

            var id = getId(entity);
            if (id <= 0) return entity; // entity mới (identity) -> để EF tự Add theo graph

            var tracked = _context.ChangeTracker.Entries<T>()
                .Select(e => e.Entity)
                .FirstOrDefault(e => getId(e) == id);

            if (tracked != null) return tracked;

            _context.Attach(entity);
            return entity;
        }

        public async Task<Phim?> GetBySlugForUpdateAsync(string slug, CancellationToken ct = default)
        {
            return await _context.Phim
                .Include(p => p.TapPhims)
                .FirstOrDefaultAsync(p => p.NguonP == slug, ct);
        }

        public async Task<Phim> CreateAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            // ===== Guard null list =====
            phim.PhanLoaiTheLoais ??= new List<PhanLoaiTheLoai>();
            phim.DienVienCuaPhims ??= new List<DienVienCuaPhim>();
            phim.DaoDienCuaPhims ??= new List<DaoDienCuaPhim>();

            // ===== Ngôn ngữ / quốc gia =====
            phim.NgonNgu = AttachOrGetTracked(phim.NgonNgu, x => x.NgonNguId);
            phim.QuocGia = AttachOrGetTracked(phim.QuocGia, x => x.QuocGiaId);

            // ===== Thể loại (join) =====
            foreach (var pl in phim.PhanLoaiTheLoais)
            {
                pl.TheLoai = AttachOrGetTracked(pl.TheLoai, x => x.TheLoaiId);
                if (pl.TheLoai != null) pl.TheLoaiId = pl.TheLoai.TheLoaiId; //  set FK
            }

            // ===== Diễn viên (join) =====
            foreach (var link in phim.DienVienCuaPhims)
            {
                link.DienVien = AttachOrGetTracked(link.DienVien, x => x.DienVienId);
                if (link.DienVien != null) link.DienVienId = link.DienVien.DienVienId; // set FK
            }

            // ===== Đạo diễn (join) =====
            foreach (var link in phim.DaoDienCuaPhims)
            {
                link.DaoDien = AttachOrGetTracked(link.DaoDien, x => x.DaoDienId);
                if (link.DaoDien != null) link.DaoDienId = link.DaoDien.DaoDienId; // ✅ set FK
            }

            // ===== Dedup join lists (tránh trùng key composite) =====
            phim.PhanLoaiTheLoais = phim.PhanLoaiTheLoais
                .GroupBy(x =>
                    x.TheLoaiId > 0
                        ? $"ID:{x.TheLoaiId}"
                        : $"KEY:{SlugHelper.NormalizeSearchText(x.TheLoai?.MoTa ?? x.TheLoai?.TenTL ?? "")}"
                )
                .Select(g => g.First())
                .ToList();

            phim.DienVienCuaPhims = phim.DienVienCuaPhims
                .GroupBy(x =>
                    x.DienVienId > 0
                        ? $"ID:{x.DienVienId}"
                        : $"KEY:{SlugHelper.NormalizeSearchText(x.DienVien?.MoTaDV ?? x.DienVien?.TenDV ?? "")}"
                )
                .Select(g => g.First())
                .ToList();

            phim.DaoDienCuaPhims = phim.DaoDienCuaPhims
                .GroupBy(x =>
                    x.DaoDienId > 0
                        ? $"ID:{x.DaoDienId}"
                        : $"KEY:{SlugHelper.NormalizeSearchText(x.DaoDien?.MoTaDD ?? x.DaoDien?.TenDD ?? "")}"
                )
                .Select(g => g.First())
                .ToList();

            // ===== Save =====
            _context.Phim.Add(phim);
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
        public async Task<(List<Phim> phims, int total, int last)> SearchPhimsAsync(string keyword, int page, int limit, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 10 : limit;

            var k = (keyword ?? "").Trim();
            if (string.IsNullOrWhiteSpace(k))
                return (new List<Phim>(), 0, 0);

            // NOTE: chọn collation phù hợp DB bạn, thường dùng Latin1_General_CI_AI
            // CI = case-insensitive, AI = accent-insensitive (bỏ dấu)
            var query = _context.Phim
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.Like(EF.Functions.Collate(p.TenP ?? "", "Latin1_General_CI_AI"), $"%{k}%")
                    || EF.Functions.Like(EF.Functions.Collate(p.TenCTSXP ?? "", "Latin1_General_CI_AI"), $"%{k}%")
                    || EF.Functions.Like(EF.Functions.Collate(p.NguonP ?? "", "Latin1_General_CI_AI"), $"%{k}%")
                )
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .Include(p => p.PhanLoaiTheLoais).ThenInclude(pl => pl.TheLoai)
                .Include(p => p.DaoDienCuaPhims).ThenInclude(dp => dp.DaoDien)
                .Include(p => p.DienVienCuaPhims).ThenInclude(dv => dv.DienVien)
                .Include(p => p.PhanLoaiTheTags).ThenInclude(pt => pt.TheTag);

            var total = await query.CountAsync(ct);
            var last = (int)Math.Ceiling((double)total / limit);

            var result = await query
                .OrderByDescending(p => p.PhimId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(ct);

            return (result, total, last);
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
