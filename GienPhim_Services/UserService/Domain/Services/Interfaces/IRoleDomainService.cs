using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Shared.Kernel;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Domain.Services.Interfaces
{
    public interface IRoleDomainService
    {
        void EnsureCanCreate(Role role); // Kiểm tra các điều kiện để tạo Role mới  
    }
}
