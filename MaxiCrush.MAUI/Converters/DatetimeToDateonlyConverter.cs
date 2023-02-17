using System.Globalization;

namespace MaxiCrush.MAUI.Converters;

public class DatetimeToDateonlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DateOnly.FromDateTime((DateTime)value).ToString(culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DateOnly.Parse((string)value).ToDateTime(TimeOnly.MinValue);
    }
}
