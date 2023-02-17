using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.MVVM.Views;
using MaxiCrush.MAUI.Services;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class WhatIsYourNameViewModel : ObservableObject
{
    [ObservableProperty]
    private string _firstname;

    private readonly IUserBuilder _accountBuilder;

    public WhatIsYourNameViewModel(IUserBuilder accountBuilder)
    {
        _accountBuilder = accountBuilder;
    }

    [RelayCommand]
    private async void GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void GoNext()
    {
        _accountBuilder.WithFirstname(Firstname);
        await Shell.Current.GoToAsync(nameof(WhatIsYourBirthdayView));
    }
}
