using MaxiCrush.Application.Common.Interfaces.Authentication;
using MaxiCrush.Application.Common.Settings;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Cryptography;
using System.Text;

namespace MaxiCrush.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private readonly SecuritySettings _securitySettings;

    public PasswordHasher(SecuritySettings securitySettings)
        => _securitySettings = securitySettings;

    public byte[] ComputeHash(string password)
    {
        using var hmac = new HMACSHA512();

        hmac.Key = Encoding.UTF8.GetBytes(_securitySettings.Key);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return hash;
    }

    public bool Equivalent(byte[] left, byte[] right)
    {
        return left.SequenceEqual(right);
    }
}
