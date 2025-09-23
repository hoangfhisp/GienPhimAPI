
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Exceptions.Invalid;
using GienPhim_Services.UserService.Domain.Services.Interfaces;

namespace GienPhim_Services.UserService.Domain.Services
{
    public class RoleDomainService : IRoleDomainService
    {

        public void EnsureCanCreate(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
            {
                throw new InvalidRoleNameNullException(role.RoleName);
            }
        }
    }
}
