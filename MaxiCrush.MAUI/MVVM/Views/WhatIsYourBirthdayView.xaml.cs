using MaxiCrush.MAUI.Controls;
using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class WhatIsYourBirthdayView : RegistrationPage
{
	public WhatIsYourBirthdayView(WhatIsYourBirthdayViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}