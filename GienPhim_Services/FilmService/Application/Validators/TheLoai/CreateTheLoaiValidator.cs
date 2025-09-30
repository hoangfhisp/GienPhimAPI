using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Validators.TheLoai
{
    public class CreateTheLoaiValidator : IValidator<TheLoaiCreateDTO>
    {
        private readonly ITheLoaiRepository _theLoaiRepository;

        public CreateTheLoaiValidator(ITheLoaiRepository theLoaiRepository)
        {
            _theLoaiRepository = theLoaiRepository;
        }

        public async Task ValidateAsync(TheLoaiCreateDTO dto, CancellationToken cancellationToken = default)
        {
            // Kiểm tra tên không rỗng
            if (string.IsNullOrWhiteSpace(dto.TenTL))
                throw new InvalidTheLoaiException("Tên thể loại không được để trống.");

            // Kiểm tra độ dài tên
            if (dto.TenTL.Length > 200)
                throw new InvalidTheLoaiException("Tên thể loại quá dài (tối đa 200 ký tự).");

        }
    }
}
