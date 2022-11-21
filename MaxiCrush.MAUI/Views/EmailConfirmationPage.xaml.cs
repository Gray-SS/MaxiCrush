using MaxiCrush.MAUI.Exceptions;
using MaxiCrush.MAUI.Services;
using MaxiCrush.Rest;
using MaxiCrush.Rest.Exceptions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace MaxiCrush.MAUI.Views;

public partial class EmailConfirmationPage : ContentPage
{
    private readonly RestClient _restClient;
    private readonly UserBuilder _userBuilder;

	public EmailConfirmationPage(RestClient restService, UserBuilder userBuilder)
	{
		InitializeComponent();

        _restClient = restService;
        _userBuilder = userBuilder;

    }

    private async void ContinueButtonClicked(object sender, EventArgs e)
    {
        var code = codeEntryInput.Text;
        var email = _userBuilder.Email;

        if (string.IsNullOrEmpty(code))
        {
            await DisplayAlert("Oups", "Je pense que tu as oublié de remplir la case !", "OK");
            return;
        }

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Oups", "Tu n'as pas de connexion internet !", "OK");
            return;
        }


        var error = await Utils.HandleRequest(async () =>
        {
            confirmButton.IsInProgress = true;

            var verified = await _restClient.VerifyConfirmationCodeAsync(email, code);

            if (verified)
            {
                //_userBuilder.Code = code;
                await Shell.Current.GoToAsync(nameof(PersonnalDataRegistrationPage));
            }

            confirmButton.IsInProgress = false;
        });

        if (error != null)
        {
            await DisplayAlert("Oups", error.Value.Message, "OK");
        }
    }
}