using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Validators.NgonNgu
{
    public class CreateNgonNguValidator : IValidator<NgonNguCreateDTO>
    {
        private readonly INgonNguRepository _ngonNguRepository;

        public CreateNgonNguValidator(INgonNguRepository ngonNguRepository)
        {
            _ngonNguRepository = ngonNguRepository;
        }

        public async Task ValidateAsync(NgonNguCreateDTO dto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.TenNN))
                throw new InvalidNgonNguException("Tên ngôn ngữ không được để trống.");

            if (dto.TenNN.Length > 100)
                throw new InvalidNgonNguException("Tên ngôn ngữ quá dài.");
        }
    }
}
