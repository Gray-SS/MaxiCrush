using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.AdminViewControl.Views;
using MaxiCrush.Contracts;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Users;
using MaxiCrush.Rest;
using MaxiCrush.Rest.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace MaxiCrush.AdminViewControl.ViewModels;

public class CheckedItem<T>
{
    public T Item { get; set; }
    public bool IsChecked { get; set; }
}

public partial class RoleEditViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private int _power;

    [ObservableProperty]
    private ObservableCollection<CheckedItem<PermissionDto>> _permissions;

    private RoleDto _role;
    private readonly RestClient _restClient;

    public RoleEditViewModel(RoleDto role, RestClient restClient)
    {
        _role = role;
        _restClient = restClient;

        Name = role.Name;
        Power = role.Power;

        ReloadPermissions();
    }

    private async void ReloadPermissions()
    {
        var permissions = (await _restClient.GetAllPermissionsAsync()).Select(x =>
        {
            if (_role.Permissions.Any(y => x.Name == y.Name))
                return new CheckedItem<PermissionDto>()
                {
                    IsChecked = true,
                    Item = x
                };

            return new CheckedItem<PermissionDto>()
            {
                IsChecked = false,
                Item = x
            };
        });

        Permissions = new ObservableCollection<CheckedItem<PermissionDto>>(permissions);
    }

    [RelayCommand]
    public void GoBack()
    {
        MainViewModel.Current.NavigateBack();
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        await Utils.HandleRequest(async () =>
        {
            if (Name != _role.Name ||
                Power != _role.Power ||
                Permissions.Count(x => x.IsChecked) != _role.Permissions.Count())
            {
                await _restClient.UpdateRoleAsync(_role.Id, Name, Power, Permissions.Where(x => x.IsChecked).Select(x => x.Item.Id).ToArray());

                MessageBox.Show("Role successfully updated !");
            }
        });
    }
}
