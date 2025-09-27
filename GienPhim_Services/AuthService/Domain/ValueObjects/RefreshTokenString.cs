using GienPhim_Services.AuthService.Domain.Exceptions.Invalid;

namespace GienPhim_Services.AuthService.Domain.ValueObjects
{
    public class RefreshTokenString
    {
        public string Value { get; }

        public RefreshTokenString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidRefreshTokenException();

            Value = value;
        }

        public override string ToString() => Value;
    }
}
