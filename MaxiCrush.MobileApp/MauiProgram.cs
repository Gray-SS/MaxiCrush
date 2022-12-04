using MaxiCrush.MobileApp.Services;
using MaxiCrush.MobileApp.Views;
using MaxiCrush.Rest;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

namespace MaxiCrush.MobileApp
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

            builder.Services.AddSingleton(new RestClient(RestClient.HostType.Azure));

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