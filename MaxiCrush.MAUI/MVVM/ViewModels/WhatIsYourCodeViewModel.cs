using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.MAUI.Helpers;
using MaxiCrush.MAUI.MVVM.Views;
using MaxiCrush.MAUI.Services;
using System.Diagnostics;
using System.Text;

namespace MaxiCrush.MAUI.MVVM.ViewModels;

public partial class WhatIsYourCodeViewModel : ObservableObject
{
    public string Email => _accountBuilder.Email;

    [ObservableProperty]
    private string _digitOne;

    [ObservableProperty]
    private string _digitTwo;

    [ObservableProperty]
    private string _digitThree;

    [ObservableProperty]
    private string _digitFour;

    [ObservableProperty]
    private Color _digitOneStroke;

    [ObservableProperty]
    private Color _digitTwoStroke;

    [ObservableProperty]
    private Color _digitThreeStroke;

    [ObservableProperty]
    private Color _digitFourStroke;

    private int _position;

    private readonly Color _selectedColor;
    private readonly Color _unselectedColor;
    private readonly IAlertDisplayer _alertDisplayer;
    private readonly IUserBuilder _accountBuilder;

    public WhatIsYourCodeViewModel(IUserBuilder accountBuilder,
                                   IAlertDisplayer alertDisplayer)
    {
        _accountBuilder = accountBuilder;
        _alertDisplayer = alertDisplayer;

        _unselectedColor = Color.FromArgb("#4F4F4F");
        _selectedColor = ResourceHelper.Get<Color>("Primary");

        UpdateColors();

        DigitOne = "0";
        DigitTwo = "0";
        DigitThree = "0";
        DigitFour = "0";
    }

    [RelayCommand]
    private async void GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void UpdateColors()
    {
        switch (_position)
        {
            case 0:
                DigitOneStroke = _selectedColor;
                DigitTwoStroke = _unselectedColor;
                DigitThreeStroke = _unselectedColor;
                DigitFourStroke = _unselectedColor;
                break;

            case 1:
                DigitOneStroke = _unselectedColor;
                DigitTwoStroke = _selectedColor;
                DigitThreeStroke = _unselectedColor;
                DigitFourStroke = _unselectedColor;
                break;

            case 2:
                DigitOneStroke = _unselectedColor;
                DigitTwoStroke = _unselectedColor;
                DigitThreeStroke = _selectedColor;
                DigitFourStroke = _unselectedColor;
                break;

            case 3:
                DigitOneStroke = _unselectedColor;
                DigitTwoStroke = _unselectedColor;
                DigitThreeStroke = _unselectedColor;
                DigitFourStroke = _selectedColor;
                break;

            default:
                DigitOneStroke = _unselectedColor;
                DigitTwoStroke = _unselectedColor;
                DigitThreeStroke = _unselectedColor;
                DigitFourStroke = _unselectedColor;
                break;
        }
    }

    [RelayCommand]
    private void WriteDigit(string input)
    {
        if (char.IsDigit(input[0]))
        {
            switch (_position)
            {
                case 0: DigitOne = input; break;
                case 1: DigitTwo = input; break;
                case 2: DigitThree = input; break;
                case 3: DigitFour = input; break;
            }

            if (_position < 4) _position += 1;
            UpdateColors();
        }
        else
        {
            if (input == IconFont.ArrowLeft)
            {
                if (_position > 0) _position -= 1;
                UpdateColors();

                switch (_position)
                {
                    case 0: DigitOne = "0"; break;
                    case 1: DigitTwo = "0"; break;
                    case 2: DigitThree = "0"; break;
                    case 3: DigitFour = "0"; break;
                }
            }
        }
    }

    private string GetCode()
    {
        var sb = new StringBuilder();
        sb.Append(DigitOne);
        sb.Append(DigitTwo);
        sb.Append(DigitThree);
        sb.Append(DigitFour);
        return sb.ToString();
    }

    [RelayCommand]
    private async void GoNext()
    {
        var code = GetCode();

        if (_position != 4)
        {
            await _alertDisplayer.ShowAlertAsync("Oups !", "Vous n'avez pas rempli toute les cases !", "Ok");
            return;
        }

        await Shell.Current.GoToAsync(nameof(WhatIsYourNameView));
    }
}