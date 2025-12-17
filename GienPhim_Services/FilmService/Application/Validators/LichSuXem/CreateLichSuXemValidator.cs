using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;



namespace GienPhim_Services.FilmService.Application.Validators.LichSuXem
{
    public class CreateLichSuXemValidator : IEntityValidator<LichSuXemCreateDTO,string>
    {
        private readonly ITapPhimRepository _tapPhimRepository;
        public CreateLichSuXemValidator(ITapPhimRepository tapPhimRepository)
        {
            _tapPhimRepository = tapPhimRepository;
        }

        public async Task ValidateAsync(LichSuXemCreateDTO dto, CancellationToken cancellationToken = default)
        {
        }

        public async Task<string> ValidateAndGetAsync(LichSuXemCreateDTO dto, CancellationToken cancellationToken = default)
        {
            var tapPhim = await _tapPhimRepository
                .FindByTapSugAndPhimId(dto.TapVuaXem, dto.PhimId, cancellationToken);

            if (tapPhim == null)
                throw new TapPhimNotFoundException();

            return tapPhim.TapId;
        }

    }
}
