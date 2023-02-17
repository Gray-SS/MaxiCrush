using CommunityToolkit.Maui.Core;
using MaxiCrush.MAUI.MVVM.ViewModels;
using MaxiCrush.MAUI.MVVM.Views;
using MaxiCrush.MAUI.Services;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;

namespace MaxiCrush.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddScoped<IAlertDisplayer, AlertDisplayer>();
            builder.Services.AddSingleton<IUserBuilder, UserBuilder>();

            builder.Services.AddTransient<WelcomeView>();
            builder.Services.AddTransient<WelcomeViewModel>();

            builder.Services.AddTransient<WhatIsYourEmailView>();
            builder.Services.AddTransient<WhatIsYourEmailViewModel>();

            builder.Services.AddTransient<WhatIsYourCodeView>();
            builder.Services.AddTransient<WhatIsYourCodeViewModel>();

            builder.Services.AddTransient<RegistrationRulesView>();
            builder.Services.AddTransient<RegistrationRulesViewModel>();

            builder.Services.AddTransient<WhatIsYourNameView>();
            builder.Services.AddTransient<WhatIsYourNameViewModel>();

            builder.Services.AddTransient<WhatIsYourBirthdayView>();
            builder.Services.AddTransient<WhatIsYourBirthdayViewModel>();

            builder.Services.AddTransient<WhatIsYourGenderView>();
            builder.Services.AddTransient<WhatIsYourGenderViewModel>();

            builder.Services.AddTransient<WhatIsYourGenderInterestView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}