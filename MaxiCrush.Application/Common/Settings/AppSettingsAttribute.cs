namespace MaxiCrush.Application.Common.Settings;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AppSettingsAttribute : Attribute
{
    public string Name { get; private set; }

    public AppSettingsAttribute(string name)
    {
        Name = name;
    }
}
