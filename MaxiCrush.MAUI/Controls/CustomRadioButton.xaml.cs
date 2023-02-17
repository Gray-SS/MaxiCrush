using Microsoft.Maui;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MaxiCrush.MAUI.Controls;

public partial class CustomRadioButton : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Text),
		typeof(string),
		typeof(CustomRadioButton));

	public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(
		nameof(GroupName),
		typeof(string),
		typeof(CustomRadioButton));

	public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
		nameof(CornerRadius),
		typeof(int),
		typeof(CustomRadioButton),
		15);

    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
		nameof(BorderWidth),
		typeof(double),
		typeof(CustomRadioButton),
		0d);

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
		nameof(BorderColor),
		typeof(Color),
		typeof(CustomRadioButton),
		Colors.Black);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
		nameof(TextColor),
		typeof(Color),
		typeof(CustomRadioButton),
		Colors.Black);

    public static readonly BindableProperty OnSelectedBorderColorProperty = BindableProperty.Create(
		nameof(OnSelectedBorderColor),
		typeof(Color),
		typeof(CustomRadioButton),
		Colors.Gray);

    public static readonly BindableProperty OnSelectedBackgroundColorProperty = BindableProperty.Create(
		nameof(OnSelectedBackgroundColor),
		typeof(Color),
		typeof(CustomRadioButton),
		Colors.Gray);

    public static readonly BindableProperty OnSelectedTextColorProperty = BindableProperty.Create(
        nameof(OnSelectedTextColor),
        typeof(Color),
        typeof(CustomRadioButton),
        Colors.White);

	public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
		nameof(IsChecked),
		typeof(bool),
		typeof(CustomRadioButton),
		false);

	public static readonly BindableProperty CommandProperty = BindableProperty.Create(
		nameof(Command),
		typeof(ICommand),
		typeof(CustomRadioButton),
		propertyChanged: OnCommandPropertyChanged);

    public CustomRadioButton()
	{
		InitializeComponent();
	}

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public string GroupName
	{
		get => (string)GetValue(GroupNameProperty);
		set => SetValue(GroupNameProperty, value);
	}

	public int CornerRadius
	{
		get => (int)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

	public Color TextColor
	{
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

	public Color OnSelectedBorderColor
	{
		get => (Color)GetValue(OnSelectedBorderColorProperty);
        set => SetValue(OnSelectedBorderColorProperty, value);
    }

    public Color OnSelectedBackgroundColor
    {
        get => (Color)GetValue(OnSelectedBackgroundColorProperty);
        set => SetValue(OnSelectedBackgroundColorProperty, value);
    }

    public Color OnSelectedTextColor
    {
        get => (Color)GetValue(OnSelectedTextColorProperty);
        set => SetValue(OnSelectedTextColorProperty, value);
    }

	public bool IsChecked
	{
		get => (bool)GetValue(IsCheckedProperty);
		set => SetValue(IsCheckedProperty, value);
	}

	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	{
		((CustomRadioButton)bindable).Command = (ICommand)newValue;
	}
}