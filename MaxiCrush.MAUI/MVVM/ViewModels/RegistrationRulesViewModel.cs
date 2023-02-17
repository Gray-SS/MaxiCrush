using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.MVVM.Views;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class RegistrationRulesViewModel : ObservableObject
{
    [RelayCommand]
    private async void GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void GotoWhatIsYourNameView()
    {
        await Shell.Current.GoToAsync(nameof(WhatIsYourNameView));
    }
}