using MaxiCrush.Application.Common.Interfaces.Authentication;
using MaxiCrush.Application.Common.Settings;
using MaxiCrush.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaxiCrush.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(securityKey,
                                                        SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Firstname),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.Lastname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var permissions in user.Role.Permissions)
        {
            claims.Add(new Claim("perms", permissions.Name));
        }

        var securityToken = new JwtSecurityToken(claims: claims,
                                                 issuer: _jwtSettings.Issuer,
                                                 audience: _jwtSettings.Audience,
                                                 expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                                                 signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
