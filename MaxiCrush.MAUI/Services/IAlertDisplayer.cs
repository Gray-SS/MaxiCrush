namespace MaxiCrush.MAUI.Services;

public interface IAlertDisplayer
{
    Task ShowAlertAsync(string title, string message, string cancel);
    Task ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No");
    void ShowAlert(string title, string message, string cancel);
    void ShowConfirmation(string title, string message, string accept = "Yes", string cancel = "No");
}
