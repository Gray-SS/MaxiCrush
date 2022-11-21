using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.AdminViewControl.Views;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Rest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MaxiCrush.AdminViewControl.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<UserDto> _users;

    [ObservableProperty]
    private object _selectedItem;

    private readonly RestClient _restClient;

    public UsersViewModel(RestClient restClient)
    {
        _restClient = restClient;

        RefreshUsersAsync();
    }

    [RelayCommand]
    public async void RefreshUsersAsync()
    {
        await Utils.HandleRequest(async () =>
        {
            var users = await _restClient.GetAllUsersAsync();
            Users = new ObservableCollection<UserDto>(users);
        });
    }

    [RelayCommand]
    public async Task GoToUser()
    {
        var user = (UserDto)SelectedItem;

        MainViewModel.Current.NavigateTo(new UserEditViewModel(user, _restClient));
    }

    [RelayCommand]
    public async Task AddUser()
    {
        var window = new AddUserWindow();
        window.DataContext = new AddUserViewModel(window, _restClient);
        window.OnUserCreated += () => RefreshUsersAsync();

        window.Show();
    }

    [RelayCommand]
    public async Task DeleteUser()
    {
        var messageBoxResult = MessageBox.Show("Are you sure you want to delete this user ?", "Delete this user ?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        if (messageBoxResult == MessageBoxResult.No)
            return;

        var selectedUser = (UserDto)SelectedItem;

        if (selectedUser == null)
            return;

        await Utils.HandleRequest(async () =>
        {
            await _restClient.DeleteUserAsync(selectedUser.Id);

            MessageBox.Show("User successfully deleted.");
            RefreshUsersAsync();
        });
    }
}
