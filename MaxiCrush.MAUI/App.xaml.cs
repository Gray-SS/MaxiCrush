using MaxiCrush.MAUI.Views;

namespace MaxiCrush.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}