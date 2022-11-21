using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Roles;
using MaxiCrush.Rest;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace MaxiCrush.AdminViewControl;

public partial class AddRoleViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private int _power;

    private readonly AddRoleWindow _attachedWindow;
    private readonly RestClient _restClient;

    public AddRoleViewModel(AddRoleWindow attachedWindow, RestClient restClient)
    {
        _restClient = restClient;
        _attachedWindow = attachedWindow;
    }

    [RelayCommand]
    public async Task AddRole()
    {
        await Utils.HandleRequest(async () =>
        {
            var role = await _restClient.CreateRoleAsync(Name, Power);

            _attachedWindow.OnRoleCreated?.Invoke();
            MessageBox.Show("Role successfully created !");
            _attachedWindow.Close();
        });
    }
}

public partial class AddRoleWindow : Window
{
    public Action? OnRoleCreated;

    public AddRoleWindow()
    {
        InitializeComponent();
    }
}
