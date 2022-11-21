using MaxiCrush.Contracts.Dto;

namespace MaxiCrush.Contracts.Authentication.Login;

public record LoginResponse(
    string Token,
    UserDto User);
