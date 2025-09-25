using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Validators.DaoDien
{
    public class CreateDaoDienValidator : IValidator<DaoDienCreateDTO>
    {
        private readonly IDaoDienRepository _daoDienRepository;

        public CreateDaoDienValidator(IDaoDienRepository daoDienRepository)
        {
            _daoDienRepository = daoDienRepository;
        }

        public async Task ValidateAsync(DaoDienCreateDTO dto, CancellationToken cancellationToken = default)
        {
            // Kiểm tra tên không rỗng hoặc các điều kiện khác
            if (string.IsNullOrWhiteSpace(dto.TenDD))
                throw new InvalidDaoDienException("Tên đạo diễn không được để trống.");

            // Kiểm tra các điều kiện khác như độ dài tên, mô tả, v.v.
            if (dto.TenDD.Length > 100)  // Ví dụ: giới hạn độ dài tên
                throw new InvalidDaoDienException("Tên đạo diễn quá dài.");

        }
    }
}
