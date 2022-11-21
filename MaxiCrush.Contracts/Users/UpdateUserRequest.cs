namespace MaxiCrush.Contracts.Users;

public record UpdateUserRequest(
    string? Firstname = null,
    string? Lastname = null,
    string? Gender = null,
    string? GenderInterest = null);
