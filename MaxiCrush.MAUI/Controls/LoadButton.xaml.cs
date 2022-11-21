using Android.Graphics.Fonts;
using System;
using System.Windows.Input;

namespace MaxiCrush.MAUI.Controls;

public partial class LoadButton : Frame
{
    public LoadButton()
    {
        InitializeComponent();
    }
    public event EventHandler<EventArgs> Tapped;

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        propertyName: nameof(Command),
        returnType: typeof(ICommand),
        declaringType: typeof(LoadButton),
        defaultBindingMode: BindingMode.TwoWay);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set { SetValue(CommandProperty, value); }
    }

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

    #region SpacingBetweenItem Property

    public static readonly BindableProperty SpacingBetweenItemsProperty = BindableProperty.Create(
    propertyName: nameof(SpacingBetweenItems),
    returnType: typeof(int),
    declaringType: typeof(LoadButton),
    defaultValue: 10,
    defaultBindingMode: BindingMode.TwoWay);

    public int SpacingBetweenItems
    {
        get => (int)GetValue(SpacingBetweenItemsProperty);
        set { SetValue(SpacingBetweenItemsProperty, value); }
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

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(LoadButton),
        defaultValue: "",
        defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(LoadButton),
        defaultValue: "Please wait...",
        defaultBindingMode: BindingMode.TwoWay);

    public string LoadingText
    {
        get => (string)GetValue(LoadingTextProperty);
        set { SetValue(LoadingTextProperty, value); }
    }

    public static readonly BindableProperty IsInProgressProperty = BindableProperty.Create(
       propertyName: nameof(IsInProgress),
       returnType: typeof(bool),
       declaringType: typeof(LoadButton),
       defaultValue: false,
       propertyChanged: IsInProgressPropertyChanged);

    private static void IsInProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (LoadButton)bindable;
        if (newValue != null)
        {
            bool isInProgress = (bool)newValue;
            if (isInProgress)
                controls.lblButtonText.Text = controls.LoadingText;
            else
                controls.lblButtonText.Text = controls.Text;
        }
    }

    public bool IsInProgress
    {
        get => (bool)GetValue(IsInProgressProperty);
        set { SetValue(IsInProgressProperty, value); }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Tapped?.Invoke(sender, e);
    }
}