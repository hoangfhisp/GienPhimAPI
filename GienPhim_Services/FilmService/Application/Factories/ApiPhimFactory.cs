using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class ApiPhimFactory : IApiPhimFactory
    {
        private readonly IApiPhimMapper _mapper;
        private readonly IPhimDomainService _phimDomainService;

        public ApiPhimFactory(IApiPhimMapper mapper, IPhimDomainService phimDomainService)
        {
            _mapper = mapper;
            _phimDomainService = phimDomainService;
        }

        /// <summary>
        /// 🟢 Tạo entity Phim từ DTO (dùng navigation entity thay vì Id)
        /// </summary>
        public Phim CreatePhim(ApiPhimDTO dto, NgonNgu ngonNgu, QuocGia quocGia, string slug)
        {
            var phim = _mapper.ToPhimEntity(dto, ngonNgu, quocGia, slug);
            _phimDomainService.EnsureCanCreateApiFilm(phim);
            return phim;
        }

        /// 🟢 Tạo entity Ngôn ngữ từ DTO
        public NgonNgu CreateNgonNgu(ApiPhimDTO dto)
            => _mapper.ToNgonNguEntity(dto);

        /// 🟢 Tạo entity Quốc gia từ DTO
        public QuocGia CreateQuocGia(ApiPhimDTO dto)
            => _mapper.ToQuocGiaEntity(dto);

        /// 🟢 Tạo danh sách thể loại từ DTO
        public List<TheLoai> CreateTheLoais(ApiPhimDTO dto)
            => _mapper.ToTheLoaiEntities(dto);

        /// 🟢 Tạo danh sách diễn viên từ DTO
        public List<DienVien> CreateDienViens(ApiPhimDTO dto)
            => _mapper.ToDienVienEntities(dto);

        /// 🟢 Tạo danh sách đạo diễn từ DTO
        public List<DaoDien> CreateDaoDiens(ApiPhimDTO dto)
            => _mapper.ToDaoDienEntities(dto);

        /// 🟢 Tạo danh sách tập phim từ DTO
        public List<TapPhim> CreateTapPhims(ApiPhimDTO dto, int phimId)
            => _mapper.ToTapPhimEntities(dto, phimId);
    }
}
