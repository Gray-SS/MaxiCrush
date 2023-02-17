namespace MaxiCrush.MAUI.Helpers;

public static class ResourceHelper
{
    public static T Get<T>(string resourceName)
    {
        if (Application.Current.Resources.TryGetValue(resourceName, out var result))
            return (T?)result;

        return default;
    }
}
