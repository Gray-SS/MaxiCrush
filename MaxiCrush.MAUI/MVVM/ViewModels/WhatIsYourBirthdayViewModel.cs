using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.MVVM.Models;
using MaxiCrush.MAUI.MVVM.Views;
using MaxiCrush.MAUI.Services;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class WhatIsYourBirthdayViewModel : ObservableObject
{
    [ObservableProperty]
    private Gender _gender;

    [ObservableProperty]
    private DateTime _maximumDatetime;

    [ObservableProperty]
    private DateTime _currentDatetime;

    private readonly IUserBuilder _userBuilder;

    public WhatIsYourBirthdayViewModel(IUserBuilder userBuilder)
    {
        _userBuilder = userBuilder;

        CurrentDatetime = DateTime.Now;
        MaximumDatetime = DateTime.Now;
    }

    [RelayCommand]
    private async void GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void GoNext()
    {
        _userBuilder.WithBirthday(DateOnly.FromDateTime(CurrentDatetime));
        await Shell.Current.GoToAsync(nameof(WhatIsYourGenderView));
    }
}
