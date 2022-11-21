namespace MaxiCrush.Contracts.Authentication.Register;

public record RegisterRequest(
    string Firstname,
    string Lastname,
    string Email,
    string Password,
    string Biography,
    string Gender,
    string GenderInterest,
    DateTime Birthday,
    string? Code = null);