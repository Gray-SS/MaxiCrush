using Microsoft.Maui.Controls;

namespace MaxiCrush.MobileApp.Views;

public partial class WelcomePage : ContentPage
{
	private bool _animated;

    public WelcomePage()
	{
		InitializeComponent();

        SetupControlsForAnimation();
    }

	protected override async void OnAppearing()
	{
		if (_animated)
			return;

        _animated = true;
        SetupControlsForAnimation();
        await AnimateAsync();
	}

	protected override void OnDisappearing()
	{
		_animated = false;

		base.OnDisappearing();
	}

	private void SetupControlsForAnimation()
	{
		placementTextLabel.Opacity = 0;
		placementTextLabel.TranslationX = 0;
		placementTextLabel.TranslationY = -50;
	}

	private async Task AnimateAsync()
	{
		var placementTask = AnimatePlacementText();

		await Task.WhenAll(placementTask);
	}

	private async Task AnimatePlacementText()
	{
		var task1 = placementTextLabel.FadeTo(1, 500, Easing.SpringIn);
		var task2 = placementTextLabel.TranslateTo(0, 0, 1000, Easing.SpringOut);

		await Task.Delay(1000);

		await Task.WhenAll(task1, task2);
	}

    private async void RegistrationButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EmailRegistrationPage));
    }

    private async void AccountRecuperationButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(AccountRecuperationPage));
	}
}