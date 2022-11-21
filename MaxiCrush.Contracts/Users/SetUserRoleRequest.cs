namespace MaxiCrush.Contracts.Users;

public record SetUserRoleRequest(
    Guid UserId,
    string Role);
