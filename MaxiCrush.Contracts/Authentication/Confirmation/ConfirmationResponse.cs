namespace MaxiCrush.Contracts.Authentication.Confirmation;

public record ConfirmationResponse(
    string SentTo,
    DateTime SentAt);
