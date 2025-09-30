using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;

namespace GienPhim_Services.FilmService.Application.Validators.TheTag
{
    public class CreateTheTagValidator : IValidator<TheTagCreateDTO>
    {
        private readonly ITheTagRepository _theTagRepository;

        public CreateTheTagValidator(ITheTagRepository theTagRepository)
        {
            _theTagRepository = theTagRepository;
        }

        public async Task ValidateAsync(TheTagCreateDTO dto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.TenT))
                throw new InvalidTheTagException("Tên thẻ tag không được để trống.");

            if (dto.TenT.Length > 100)
                throw new InvalidTheTagException("Tên thẻ tag quá dài.");

            // Nếu cần thì thêm check trùng tên (nếu có business rule)
            // var exists = await _theTagRepository.GetAllTheTagsAsync(cancellationToken);
            // if (exists.Any(t => t.TenT == dto.TenT.Trim()))
            //     throw new DuplicateTheTagException(dto.TenT);
        }
    }
}
