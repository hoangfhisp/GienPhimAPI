using GienPhim_Services.UserService.shared.Exceptions;

namespace GienPhim_Services.UserService.Domain.Exceptions.Invalid
{
    public class InvalidTenNDException : DomainException
    {
        public InvalidTenNDException(string? tenND)
            : base($"Tên người dùng không hợp lệ: '{tenND ?? string.Empty}'. Tên người dùng không được để trống và không được chứa ký tự đặc biệt.")
        {
        }
    }
}
