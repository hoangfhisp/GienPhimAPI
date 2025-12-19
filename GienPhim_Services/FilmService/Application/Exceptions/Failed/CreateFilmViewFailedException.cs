using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateFilmViewFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public CreateFilmViewFailedException(string filmViewId)
            : base($"Tạo FilmView với Id={filmViewId} thất bại.") { }
    }
}