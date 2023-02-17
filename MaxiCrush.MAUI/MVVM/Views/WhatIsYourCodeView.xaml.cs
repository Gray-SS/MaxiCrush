using MaxiCrush.MAUI.Controls;
using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class WhatIsYourCodeView : RegistrationPage
{
	public WhatIsYourCodeView(WhatIsYourCodeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}