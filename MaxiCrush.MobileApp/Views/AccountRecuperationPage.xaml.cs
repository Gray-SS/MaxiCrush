using Microsoft.Maui.Controls;

namespace MaxiCrush.MobileApp.Views;

public partial class AccountRecuperationPage : ContentPage
{
    public AccountRecuperationPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		foreach(var item in toAnimateStackLayout.Children)
		{
			if (item is not VisualElement element)
				continue;

			element.Opacity = 0;
		}

		await TransitionIn();
	}

	protected override void OnDisappearing()
	{
		foreach(var item in toAnimateStackLayout.Children)
		{
			if (item is not VisualElement element)
				continue;

			element.FadeTo(0, 500);
		}
        base.OnDisappearing();
	}

	private async Task TransitionIn()
	{
		foreach(var item in toAnimateStackLayout.Children)
        {
            if (item is not VisualElement element)
                continue;

            element.FadeTo(1, 500);
			await Task.Delay(100);
		}
	}
}