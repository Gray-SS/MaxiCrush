using Microsoft.Maui;

namespace MaxiCrush.MobileApp.Controls;

public partial class RoundedEntry : Frame
{
	public RoundedEntry()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
    propertyName: nameof(PlaceholderColor),
    returnType: typeof(Color),
    declaringType: typeof(RoundedEntry),
    defaultValue: Colors.Black,
    defaultBindingMode: BindingMode.TwoWay);

    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set { SetValue(PlaceholderColorProperty, value); }
    }


    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
    propertyName: nameof(Placeholder),
    returnType: typeof(string),
    declaringType: typeof(RoundedEntry),
    defaultValue: "Your placeholder here",
    defaultBindingMode: BindingMode.TwoWay);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set { SetValue(PlaceholderProperty, value); }
    }


    #region Text Property

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
    propertyName: nameof(Text),
    returnType: typeof(string),
    declaringType: typeof(RoundedEntry),
    defaultValue: "",
    defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set { SetValue(TextProperty, value); }
    }

    #endregion

    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
    propertyName: nameof(FontAttributes),
    returnType: typeof(FontAttributes),
    declaringType: typeof(LoadButton),
    defaultValue: FontAttributes.None,
    defaultBindingMode: BindingMode.TwoWay);

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set { SetValue(FontAttributesProperty, value); }
    }


    #region TextColor Property

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
    propertyName: nameof(TextColor),
    returnType: typeof(Color),
    declaringType: typeof(LoadButton),
    defaultValue: Color.FromArgb("#000000"),
    defaultBindingMode: BindingMode.TwoWay);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set { SetValue(TextColorProperty, value); }
    }

    #endregion

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
    propertyName: nameof(FontFamily),
    returnType: typeof(string),
    declaringType: typeof(LoadButton),
    defaultValue: "OpenSansRegular",
    defaultBindingMode: BindingMode.TwoWay);

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set { SetValue(FontFamilyProperty, value); }
    }

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
    propertyName: nameof(FontSize),
    returnType: typeof(int),
    declaringType: typeof(LoadButton),
    defaultValue: 12,
    defaultBindingMode: BindingMode.TwoWay);

    public int FontSize
    {
        get => (int)GetValue(FontSizeProperty);
        set { SetValue(FontSizeProperty, value); }
    }
}