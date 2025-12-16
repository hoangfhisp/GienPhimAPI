using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class LichSuXemDomainService : ILichSuXemDomainService
    {
        public void EnsureCanCreate(LichSuXem lsx)
        {
            if (lsx == null)
                throw new InvalidLichXuXemException("Lịch sử xem không được null");

            if (lsx.PhimId <= 0)
                throw new InvalidLichXuXemException("PhimId phải lớn hơn 0");

            if (lsx.TapPhimId == null)
                throw new InvalidLichXuXemException("Tập film id không được null");

            if(string.IsNullOrWhiteSpace(lsx.UserId))
                throw new InvalidLichXuXemException("UserId không được null hoặc rỗng");
        }
    }
}
