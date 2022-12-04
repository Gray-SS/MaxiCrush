using MaxiCrush.MobileApp.Services;
using MaxiCrush.Rest;
using MaxiCrush.Rest.Exceptions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace MaxiCrush.MobileApp.Views;

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
        try
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

            confirmButton.IsInProgress = true;
            var error = await Utils.HandleRequest(async () =>
            {
                /*
                var verified = await _restClient.VerifyConfirmationCodeAsync(email, code);

                if (verified)
                {
                    //_userBuilder.Code = code;
                    await Shell.Current.GoToAsync(nameof(PersonnalDataRegistrationPage));
                }
                */

                await Shell.Current.GoToAsync(nameof(PersonnalDataRegistrationPage));
            });

            if (error != null)
            {
                await DisplayAlert("Oups", error.Value.Message, "OK");
            }
        }
        catch (WebException)
        {
            await DisplayAlert("Oups", "Le serveur distant n'a pas répondu. Veuillez réessaiez plus tard.", "OK");
        }
        catch (Exception)
        {
            await DisplayAlert("Oups", "Une erreur inconnue est survenue", "OK");
        }
        finally
        {
            confirmButton.IsInProgress = false;
        }
    }
}