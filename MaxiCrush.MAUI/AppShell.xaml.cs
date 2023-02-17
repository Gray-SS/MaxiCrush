using MaxiCrush.MAUI.MVVM.ViewModels;
using MaxiCrush.MAUI.MVVM.Views;

namespace MaxiCrush.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RecoverAccountView), typeof(RecoverAccountView));
            Routing.RegisterRoute(nameof(WhatIsYourEmailView), typeof(WhatIsYourEmailView));
            Routing.RegisterRoute(nameof(WhatIsYourCodeView), typeof(WhatIsYourCodeView));
            Routing.RegisterRoute(nameof(RegistrationRulesView), typeof(RegistrationRulesView));
            Routing.RegisterRoute(nameof(WhatIsYourNameView), typeof(WhatIsYourNameView));
            Routing.RegisterRoute(nameof(WhatIsYourBirthdayView), typeof(WhatIsYourBirthdayView));
            Routing.RegisterRoute(nameof(WhatIsYourGenderView), typeof(WhatIsYourGenderView));
            Routing.RegisterRoute(nameof(WhatIsYourGenderInterestView), typeof(WhatIsYourGenderInterestView));
        }
    }
}