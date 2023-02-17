using MaxiCrush.MAUI.Controls;
using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class WhatIsYourNameView : RegistrationPage
{
	public WhatIsYourNameView(WhatIsYourNameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		entry.IsEnabled = false;
		entry.IsEnabled = true;
    }
}