
namespace MaxiCrush.Contracts.Users.Me;

public record UpdateMeRequest(
    int? GenderInterestedIn = null,
    string? Biography = null);
