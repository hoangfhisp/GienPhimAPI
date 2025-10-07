using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class UpdateFilmViewFailedException : BusinessException
    {
        public override int StatusCode => base.StatusCode;

        public UpdateFilmViewFailedException(string filmViewId)
            : base($"Cập nhật FilmView với Id={filmViewId} thất bại.") { }
    }
}
