using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Rest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace MaxiCrush.AdminViewControl.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public static MainViewModel Current => _lazy.Value;
    private static Lazy<MainViewModel> _lazy;

    [ObservableProperty]
    private string _fullname;

    [ObservableProperty]
    private string _role;

    [ObservableProperty]
    private object _currentView;

    private Func<object>? _lastViewFactory;
    private Func<object> _currentViewFactory;

    private Window _attachedWindow;
    private readonly RestClient _restClient;

    private readonly Dictionary<string, Func<object>> _navigationManager;

    public MainViewModel(RestClient restClient)
    {
        var host = App.Current.Host;
        CurrentView = host.Services.GetRequiredService<UsersViewModel>();

        _restClient = restClient;
        Fullname = $"{_restClient.User.Firstname} {_restClient.User.Lastname}";
        Role = _restClient.User.Role.Name;

        _navigationManager = new()
        {
            { nameof(UsersViewModel),() => host.Services.GetRequiredService<UsersViewModel>() },
            { nameof(RolesViewModel), () => host.Services.GetRequiredService<RolesViewModel>() },
            { nameof(PermissionsViewModel), () => host.Services.GetRequiredService<PermissionsViewModel>() },
        };

        _lazy = new Lazy<MainViewModel>(() => this, true);
    }

    public void SetAttachedWindow(Window window)
        => _attachedWindow = window;

    [RelayCommand]
    public async Task GoToUsers()
    {
        this.NavigateTo(nameof(UsersViewModel));
    }

    [RelayCommand]
    public async Task GoToRoles()
    {
        this.NavigateTo(nameof(RolesViewModel));
    }

    [RelayCommand]
    public async Task GoToPermissions()
    {
        this.NavigateTo(nameof(PermissionsViewModel));
    }

    [RelayCommand]
    public async Task LogOut()
    {
        await _restClient.LogoutAsync();

        var host = App.Current.Host;

        var loginWindow = host.Services.GetRequiredService<LoginWindow>();
        loginWindow.Show();

        _attachedWindow.Close();
    }

    public void NavigateBack()
    {
        if (_lastViewFactory == null)
            return;

        (_currentViewFactory, _lastViewFactory) = (_lastViewFactory, _currentViewFactory);
        CurrentView = _currentViewFactory.Invoke();
    }

    public void NavigateTo(object viewModel)
    {
        _lastViewFactory = _currentViewFactory;
        _currentViewFactory = () => viewModel;
        CurrentView = viewModel;
    }

    public void NavigateTo(string path)
    {
        if (!_navigationManager.ContainsKey(path))
            throw new KeyNotFoundException();

        _lastViewFactory = _currentViewFactory;
        _currentViewFactory = _navigationManager[path];
        CurrentView = _currentViewFactory.Invoke();
    }
}
