namespace GienPhim_Services.AuthService.Application.Interfaces.Exceptions
{
    public interface IHasHttpStatus
    {
        int StatusCode { get; }
    }
}
