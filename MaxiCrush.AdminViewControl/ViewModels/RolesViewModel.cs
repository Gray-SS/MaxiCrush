using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Rest;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup.Localizer;

namespace MaxiCrush.AdminViewControl.ViewModels;

public partial class RolesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<RoleDto> _roles;

    [ObservableProperty]
    private object _selectedItem;

    private readonly RestClient _restClient;

    public RolesViewModel(RestClient restClient)
    {
        _restClient = restClient;

        RefreshRolesAsync();
    }

    [RelayCommand]
    public async void RefreshRolesAsync()
    {
        Utils.HandleRequest(async () =>
        {
            var roles = await _restClient.GetAllRolesAsync();
            Roles = new ObservableCollection<RoleDto>(roles);
        });
    }

    [RelayCommand]
    public async Task AddRole()
    {
        var window = new AddRoleWindow();
        window.DataContext = new AddRoleViewModel(window, _restClient);
        window.OnRoleCreated += () => RefreshRolesAsync();

        window.Show();
    }

    [RelayCommand]
    public async Task DeleteRole()
    {
        var messageBoxResult = MessageBox.Show("Are you sure you want to delete this role ?", "Delete this role ?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        if (messageBoxResult == MessageBoxResult.No)
            return;

        await Utils.HandleRequest(async () =>
        {
            var role = (RoleDto)SelectedItem;
            await _restClient.DeleteRoleAsync(role.Id);

            MessageBox.Show("Successfully deleted user.");
            RefreshRolesAsync();
        });
    }

    [RelayCommand]
    public async Task EditRole()
    {
        var role = (RoleDto)SelectedItem;
        var viewModel = new RoleEditViewModel(role, _restClient);

        MainViewModel.Current.NavigateTo(viewModel);
    }
}