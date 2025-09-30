using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Validators.DienVien
{
    public class CreateDienVienValidator : IValidator<DienVienCreateDTO>
    {
        private readonly IDienVienRepository _dienVienRepository;

        public CreateDienVienValidator(IDienVienRepository dienVienRepository)
        {
            _dienVienRepository = dienVienRepository;
        }

        public async Task ValidateAsync(DienVienCreateDTO dto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.TenDV))
                throw new InvalidDienVienException("Tên diễn viên không được để trống.");

            if (dto.TenDV.Length > 100)
                throw new InvalidDienVienException("Tên diễn viên quá dài.");

        }
    }
}
