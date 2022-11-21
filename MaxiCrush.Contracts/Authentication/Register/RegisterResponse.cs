using MaxiCrush.Contracts.Dto;

namespace MaxiCrush.Contracts.Authentication.Register;

public record RegisterResponse(
    string Token,
    UserDto CreatedUser);