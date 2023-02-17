using System.Windows.Input;

namespace MaxiCrush.MAUI.Controls;

public partial class RegistrationPage : ContentPage
{
    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(RegistrationPage));

	public RegistrationPage()
	{
		InitializeComponent();
	}

    public void GoNextButtonClicked(object sender, EventArgs e)
    {
        if (Command != null && Command.CanExecute(null))
            Command.Execute(this);
    }

    public async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}