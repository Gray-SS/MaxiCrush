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
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MaxiCrush.AdminViewControl.ViewModels;

public partial class UserEditViewModel : ObservableObject
{
    [ObservableProperty]
    private object _selectedRole;

    [ObservableProperty]
    private object _selectedGender;

    [ObservableProperty]
    private object _selectedGenderInterest;

    [ObservableProperty]
    private string _firstname;

    [ObservableProperty]
    private string _lastname;

    [ObservableProperty]
    private string _biography;

    [ObservableProperty]
    private string[] _roles;

    [ObservableProperty]
    private string[] _genders;

    [ObservableProperty]
    private string _creationDateText;

    private UserDto _user;
    private readonly RestClient _restClient;

    public UserEditViewModel(UserDto user, RestClient restClient)
    {
        Firstname = user.Firstname;
        Lastname = user.Lastname;
        Biography = user.Biography;
        SelectedGender = user.Gender.ToString();
        SelectedGenderInterest = user.GenderInterestedIn.ToString();
        SelectedRole = user.Role.Name;
        CreationDateText = $"User created the {user.CreatedAt.Day:00}/{user.CreatedAt.Month:00}/{user.CreatedAt.Year}";

        _user = user;
        _restClient = restClient;

        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        Roles = (await _restClient.GetAllRolesAsync()).Select(x => x.Name).ToArray();
        Genders = Enum.GetNames<Gender>();
    }


    [RelayCommand]
    public void GoBack()
    {
        MainViewModel.Current.NavigateBack();
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        var succeed = 0;
        var toSuccess = 0;

        await Utils.HandleRequest(async () =>
        {
            if (Firstname != _user.Firstname ||
                Lastname != _user.Lastname ||
                (string)SelectedGender != _user.Gender.ToString() ||
                (string)SelectedGenderInterest != _user.GenderInterestedIn.ToString())
            {
                toSuccess++;
                _user = await _restClient.UpdateUserAsync(_user.Id, new UpdateUserRequest(Firstname,
                                                                                  Lastname,
                                                                                  (string)SelectedGender,
                                                                                  (string)SelectedGenderInterest));
                succeed++;
            }
        });

        await Utils.HandleRequest(async () =>
        {
            if ((string)SelectedRole != _user.Role.Name.ToString())
            {
                toSuccess++;
                _user = await _restClient.SetRoleAsync(_user.Id, (string)SelectedRole);
                succeed++;
            }
        });

        Firstname = _user.Firstname;
        Lastname = _user.Lastname;
        Biography = _user.Biography;
        SelectedGender = _user.Gender.ToString();
        SelectedGenderInterest = _user.GenderInterestedIn.ToString();
        SelectedRole = _user.Role.Name;

        if (toSuccess == succeed)
            MessageBox.Show("User successfully updated !");
    }
}
