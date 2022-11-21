using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Controls.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
