using MaxiCrush.AdminViewControl.ViewModels;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Rest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MaxiCrush.AdminViewControl;

public partial class LoginWindow : Window
{
    private readonly RestClient _restClient;
    
    public LoginWindow(RestClient restClient)
    {
        InitializeComponent();

        _restClient = restClient;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        await Utils.HandleRequest(async () =>
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Password;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username cannot be null or empty.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password cannot be null or empty.");
                return;
            }

            await _restClient.LoginAsync(username, password);

            if (_restClient.User.Role.Power < 500)
            {
                MessageBox.Show("Invalid credentials.");
                return;
            }

            var host = App.Current.Host;

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            this.Close();
        });
    }
}
