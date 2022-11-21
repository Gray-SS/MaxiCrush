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

        confirmButton.IsInProgress = true;

        try
        {
            var verified = await _restClient.ConfirmCodeAsync(email, code);

            if (!verified)
            {
                await DisplayAlert("Oups", "Tu as peut-être fait une faute de frappe, réessaie !", "OK");
                confirmButton.IsInProgress = false;
                return;
            }

            await Shell.Current.GoToAsync(nameof(PersonnalDataRegistrationPage));
        }
        catch(RestHttpRequestException ex)
        {
            var response = ex.Response;
            await DisplayAlert("Oups", await response.GetErrorMessageAsync(), "OK");
        }
        finally { confirmButton.IsInProgress = false; }
    }
}