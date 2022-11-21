using MaxiCrush.AdminViewControl.ViewModels;
using MaxiCrush.Rest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Windows;

namespace MaxiCrush.AdminViewControl;

public partial class App : Application
{
    public IHost Host { get; private set; }

    public static App Current => _lazy.Value;
    private static Lazy<App> _lazy;

    public App()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                                           .ConfigureServices(services =>
                                           {
                                               this.OnConfiguring(services);
                                           })
                                           .Build();

        _lazy = new Lazy<App>(() => this, true);
    }
    
    protected void OnConfiguring(IServiceCollection services)
    {
        services.AddSingleton(new RestClient(RestClientBase.HostType.Localhost));

        services.AddTransient<UsersViewModel>();
        services.AddTransient<RolesViewModel>();
        services.AddTransient<PermissionsViewModel>();

        services.AddTransient<MainViewModel>();

        services.AddTransient<MainWindow>();
        services.AddTransient<LoginWindow>();
    }


    #region Host Setup

    protected override async void OnStartup(StartupEventArgs e)
    {
        await Host.StartAsync();

        MainWindow = Host.Services.GetRequiredService<LoginWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await Host.StopAsync();
        Host.Dispose();

        base.OnExit(e);
    }

    #endregion
}
