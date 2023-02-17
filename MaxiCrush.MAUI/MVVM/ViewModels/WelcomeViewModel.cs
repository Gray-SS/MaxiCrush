using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.MVVM.Views;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class WelcomeViewModel : ObservableObject
{
    [RelayCommand]
    private async void GotoRecoverAccountView()
    {
        await Shell.Current.GoToAsync(nameof(RecoverAccountView));
    }

    [RelayCommand]
    private async void GotoWhatIsYourEmailView()
    {
        await Shell.Current.GoToAsync(nameof(WhatIsYourEmailView));
    }
}
