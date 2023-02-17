using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.Controls;
using MaxiCrush.MAUI.MVVM.Models;
using MaxiCrush.MAUI.MVVM.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class WhatIsYourGenderViewModel : ObservableObject
{
    private Gender _gender;

    [RelayCommand]
    private async void GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void GoNext()
    {
        await Shell.Current.GoToAsync(nameof(WhatIsYourGenderInterestView));
    }
}
