using GienPhim_Services.UserService.Domain.Services.Interfaces;

namespace GienPhim_Services.UserService.Domain.Services
{
    public class GuidUserNameGenerator : IGuidUserNameGenerator
    {
        public string GenerateUserName() => Guid.NewGuid().ToString("N");
    }
}
