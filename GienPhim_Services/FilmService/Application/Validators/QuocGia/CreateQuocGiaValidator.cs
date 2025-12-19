using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Validators.QuocGia
{
    public class CreateQuocGiaValidator : IValidator<QuocGiaCreateDTO>
    {
        private readonly IQuocGiaRepository _quocGiaRepository;

        public CreateQuocGiaValidator(IQuocGiaRepository quocGiaRepository)
        {
            _quocGiaRepository = quocGiaRepository;
        }

        public async Task ValidateAsync(QuocGiaCreateDTO dto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.TenQG))
                throw new InvalidQuocGiaException("Tên quốc gia không được để trống.");

            if (dto.TenQG.Length > 200)
                throw new InvalidQuocGiaException("Tên quốc gia quá dài.");
        }
    }
}
