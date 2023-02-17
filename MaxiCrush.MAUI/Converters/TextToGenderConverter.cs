using MaxiCrush.MAUI.MVVM.Models;
using System.Globalization;

namespace MaxiCrush.MAUI.Converters;

public class TextToGenderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value switch
        {
            "Je suis un homme" => Gender.Male,
            "Je suis une femme" => Gender.Female,
            "Autre" => Gender.Other,
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Gender)value switch
        {
            Gender.Male => "Je suis un homme",
            Gender.Female => "Je suis une femme",
            Gender.Other => "Autre",
            _ => string.Empty
        };
    }
}
