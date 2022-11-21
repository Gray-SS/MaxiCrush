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

public partial class PermissionsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<PermissionDto> _permissions;

    [ObservableProperty]
    private object _selectedItem;

    private readonly RestClient _restClient;

    public PermissionsViewModel(RestClient restClient)
    {
        _restClient = restClient;

        RefreshPermissionsAsync();
    }

    [RelayCommand]
    public async void RefreshPermissionsAsync()
    {
        await Utils.HandleRequest(async () =>
        {
            var permissions = await _restClient.GetAllPermissionsAsync();
            Permissions = new ObservableCollection<PermissionDto>(permissions);
        });
    }

    [RelayCommand]
    public async Task GoToPermission()
    {
        /*
        var user = (UserDto)SelectedItem;

        var detailsVM = new PermissionEditViewModel(user, _restClient);

        var permissions = await _restClient.GetAllRolesAsync();
        detailsVM.Permissions = permissions.Select(x => x.Name).ToArray();
        
        MainViewModel.Current.NavigateTo(detailsVM);
        */
    }

    [RelayCommand]
    public async Task AddPermission()
    {
        var window = new AddPermissionWindow();
        window.DataContext = new AddPermissionViewModel(window, _restClient);
        window.OnPermissionCreated += RefreshPermissionsAsync;

        window.Show();
    }

    [RelayCommand]
    public async Task DeletePermission()
    {
        var messageBoxResult = MessageBox.Show("Are you sure you want to delete this permission ?", "Delete this permission ?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        if (messageBoxResult == MessageBoxResult.No)
            return;

        var selectedUser = (PermissionDto)SelectedItem;

        if (selectedUser == null)
            return;

        await Utils.HandleRequest(async () =>
        {
            await _restClient.DeletePermissionAsync(selectedUser.Id);

            MessageBox.Show("Permission successfully deleted.");
            RefreshPermissionsAsync();
        });
    }
}
