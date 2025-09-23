namespace GienPhim_Services.AuthService.Domain.Models
{
    public class Otp
    {
        public string UserId { get; }
        public string Code { get; }
        public string Type { get; }
        public DateTime ExpiryTime { get; }

        public Otp(string userId, string code, string type, int expiryMinutes = 5)
        {
            UserId = userId;
            Code = code;
            Type = type;
            ExpiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes);
        }

        // ktra hết hạn
        public bool IsExpired() => DateTime.UtcNow > ExpiryTime;

        // ktra mã đúng và chưa hết hạn
        public bool Verify(string input) => input == Code && !IsExpired();
    }

}
