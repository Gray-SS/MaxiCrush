using Android.Views;
using MaxiCrush.MAUI.Views;
using CommunityToolkit.Mvvm;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;
using MaxiCrush.MAUI.Services;
using Java.Sql;
using MaxiCrush.MAUI.Services.Rest;

namespace MaxiCrush.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "IconFont");
                    fonts.AddFont("NexaDemo-Bold.ttf", "NexaBold");
                    fonts.AddFont("NexaDemo-Light.ttf", "NexaLight");
                    fonts.AddFont("NexaTextDemo-Light.ttf", "NexaTextLight");
                    fonts.AddFont("NexaTextDemo-Bold.ttf", "NexaTextBold");
                });

            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton<RestService>();

            builder.Services.AddSingleton<UserBuilder>();
            builder.Services.AddSingleton<AccountRecuperationPage>();
            builder.Services.AddSingleton<EmailConfirmationPage>();
            builder.Services.AddSingleton<EmailRegistrationPage>();

            builder.Services.AddTransient<WelcomePage>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}