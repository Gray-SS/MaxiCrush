using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class WelcomeView : ContentPage
{
	public WelcomeView(WelcomeViewModel welcomeVM)
	{
		InitializeComponent();
		BindingContext = welcomeVM;
	}
}