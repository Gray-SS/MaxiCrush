using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
