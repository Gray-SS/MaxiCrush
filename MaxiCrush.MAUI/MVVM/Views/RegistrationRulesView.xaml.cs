using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class RegistrationRulesView : ContentPage
{
	public RegistrationRulesView(RegistrationRulesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}