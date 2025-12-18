using System.Security.Cryptography;
using System.Text;

namespace GienPhim_Services.WalletService.Infrastructure.Payments.VnPay
{
    public static class VnPayHashHelper
    {
        public static string HmacSHA512(string key, string data)
        {
            var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
