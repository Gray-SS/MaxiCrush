using MaxiCrush.MAUI.Controls;
using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class WhatIsYourEmailView : RegistrationPage
{
	public WhatIsYourEmailView(WhatIsYourEmailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		entry.IsEnabled = false;
		entry.IsEnabled = true;
    }
}