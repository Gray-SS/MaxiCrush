using MaxiCrush.MAUI.Controls;
using MaxiCrush.MAUI.MVVM.ViewModels;

namespace MaxiCrush.MAUI.MVVM.Views;

public partial class WhatIsYourGenderView : RegistrationPage
{
	public WhatIsYourGenderView(WhatIsYourGenderViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}