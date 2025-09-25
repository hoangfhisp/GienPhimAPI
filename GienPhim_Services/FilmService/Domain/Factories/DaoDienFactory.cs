using System;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Factories
{
    public static class DaoDienFactory
    {
        public static DaoDien Create(string ten, string? moTa, DateTime nowUtc, IDaoDienDomainService domainService)
        {
            var entity = new DaoDien
            {
                TenDD = ten?.Trim() ?? string.Empty,
                MoTaDD = moTa?.Trim(),
                ThoiGianTao = nowUtc,
                ThoiGianCapNhat = nowUtc
            };

            domainService.EnsureCanCreate(entity);
            return entity;
        }

        public static void Update(DaoDien existing, string tenMoi, string? moTaMoi, DateTime nowUtc, IDaoDienDomainService domainService)
        {
            existing.TenDD = tenMoi?.Trim() ?? string.Empty;
            existing.MoTaDD = moTaMoi?.Trim();
            existing.ThoiGianCapNhat = nowUtc;

            domainService.EnsureCanUpdate(existing);
        }
    }
}
