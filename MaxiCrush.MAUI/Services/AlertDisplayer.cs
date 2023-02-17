namespace MaxiCrush.MAUI.Services;

public class AlertDisplayer : IAlertDisplayer
{
    public void ShowAlert(string title, string message, string cancel)
    {
        Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }

    public void ShowConfirmation(string title, string message, string accept = "Yes", string cancel = "No")
    {
        Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }

    public Task ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }

    public Task ShowAlertAsync(string title, string message, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}
