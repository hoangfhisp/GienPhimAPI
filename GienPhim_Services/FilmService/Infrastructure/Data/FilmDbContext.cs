using GienPhim_Services.FilmService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Data
{
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions<FilmDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DaoDienCuaPhim>()
               .HasKey(ddp => new { ddp.PhimId, ddp.DaoDienId });

            builder.Entity<PhanLoaiTheTag>()
                .HasKey(ddp => new { ddp.PhimId, ddp.TheTagId });

            builder.Entity<PhanLoaiTheLoai>()
                .HasKey(ddp => new { ddp.PhimId, ddp.TheLoaiId });

            builder.Entity<DienVienCuaPhim>()
                .HasKey(ddp => new { ddp.PhimId, ddp.DienVienId });

        }
        public DbSet<Phim> Phim { get; set; }
        public DbSet<DaoDien> DaoDien { get; set; }
        public DbSet<DaoDienCuaPhim> DaoDienCuaPhim { get; set; }
        public DbSet<TheTag> TheTag { get; set; }
        public DbSet<PhanLoaiTheTag> PhanLoaiTheTag { get; set; }
        public DbSet<TheLoai> TheLoai { get; set; }
        public DbSet<PhanLoaiTheLoai> PhanLoaiTheLoai { get; set; }
        public DbSet<DienVien> DienVien { get; set; }
        public DbSet<DienVienCuaPhim> DienVienCuaPhim { get; set; }
        public DbSet<QuocGia> QuocGia { get; set; }
        public DbSet<NgonNgu> NgonNgu { get; set; }
        public DbSet<TapPhim> TapPhim { get; set; }
        public DbSet<LichSuXem> LichSuXem { get; set; }
        public DbSet<FilmView> FilmView { get; set; }

    }
}
