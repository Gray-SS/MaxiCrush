using MaxiCrush.MAUI.Services;
using MaxiCrush.Rest;
using Newtonsoft.Json.Linq;

namespace MaxiCrush.MAUI.Views;

public partial class EmailRegistrationPage : ContentPage
{
    private readonly UserBuilder _userBuilder;
    private readonly RestClient _restClient;

    public EmailRegistrationPage(RestClient restClient, UserBuilder userBuilder)
    {
        InitializeComponent();

        _restClient = restClient;
        _userBuilder = userBuilder;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        titlePageLabel.Opacity = 0;
        descriptionPageLabel.Opacity = 0;

        await TransitionIn();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        await TransitionOut();
    }

    private async Task TransitionIn()
    {
        await Task.Delay(300);

        var task1 = titlePageLabel.FadeTo(1, 500);
        await Task.Delay(50);
        var task2 = descriptionPageLabel.FadeTo(1, 500);
        await Task.Delay(50);

        await Task.WhenAll(task1, task2);
    }

    private async Task TransitionOut()
    {
        var task1 = titlePageLabel.FadeTo(0, 500);
        await Task.Delay(50);
        var task2 = descriptionPageLabel.FadeTo(0, 500);
        await Task.Delay(50);

        await Task.WhenAll(task1, task2);
    }

    private async void ContinueButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(emailEntryInput.Text))
            return;

        confirmButton.IsInProgress = true;

        var sended = await _restClient.SendConfirmationCodeAsync(emailEntryInput.Text);

        confirmButton.IsInProgress = false;

        if (sended)
        {
            _userBuilder.Email = emailEntryInput.Text;
            await Shell.Current.GoToAsync(nameof(EmailConfirmationPage));
        }
        else
        {
            await DisplayAlert("Oups !", $"Il semble que tu aies déjà un compte. Appuie sur le bouton récupérer mon compte dans le menu principal afin de pouvoir te connecter.", "OK");
        }
    }
}