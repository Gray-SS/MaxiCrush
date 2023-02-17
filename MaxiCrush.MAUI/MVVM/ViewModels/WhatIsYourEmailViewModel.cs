using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.MVVM.Views;
using MaxiCrush.MAUI.Services;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class WhatIsYourEmailViewModel : ObservableObject
{
    [ObservableProperty]
    string _email;

    private readonly IUserBuilder _accountBuilder;

    public WhatIsYourEmailViewModel(IUserBuilder accountBuilder)
        => _accountBuilder = accountBuilder;

    [RelayCommand]
    private async void GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void GoNext()
    {
        _accountBuilder.WithEmail(Email);
        await Shell.Current.GoToAsync(nameof(WhatIsYourCodeView));
    }
}
