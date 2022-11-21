using MaxiCrush.MAUI.Views;

namespace MaxiCrush.MAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AccountRecuperationPage), typeof(AccountRecuperationPage));
		Routing.RegisterRoute(nameof(EmailRegistrationPage), typeof(EmailRegistrationPage));
		Routing.RegisterRoute(nameof(PersonnalDataRegistrationPage), typeof(PersonnalDataRegistrationPage));
		Routing.RegisterRoute(nameof(EmailConfirmationPage), typeof(EmailConfirmationPage));
    }
}