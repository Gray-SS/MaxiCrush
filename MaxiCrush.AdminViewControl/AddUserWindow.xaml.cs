using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Rest;
using MaxiCrush.Rest.Exceptions;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MaxiCrush.AdminViewControl;

public partial class AddUserViewModel : ObservableObject
{
    public string[] Genders { get; private set; }

    [ObservableProperty]
    private string _firstname;

    [ObservableProperty]
    private string _lastname;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private object _selectedGender;

    [ObservableProperty]
    private object _selectedGenderInterest;

    [ObservableProperty]
    private DateTime _birthday;

    private readonly AddUserWindow _attachedWindow;
    private readonly RestClient _restClient;

    public AddUserViewModel(AddUserWindow attachedWindow, RestClient restClient)
    {
        _restClient = restClient;
        _attachedWindow = attachedWindow;

        SelectedGender = Gender.Male.ToString();
        SelectedGenderInterest = Gender.Female.ToString();
        Genders = Enum.GetNames<Gender>();
    }

    [RelayCommand]
    public async Task AddUser()
    {
        await Utils.HandleRequest(async () =>
        {
            var user = await _restClient.RegisterAsync(Firstname, Lastname, Email, Password, (string)SelectedGender, (string)SelectedGenderInterest, Birthday);

            _attachedWindow.OnUserCreated?.Invoke();
            MessageBox.Show("User successfully created !");
            _attachedWindow.Close();
        });
    }
}

public partial class AddUserWindow : Window
{
    public Action? OnUserCreated;

    public AddUserWindow()
    {
        InitializeComponent();
    }
}
