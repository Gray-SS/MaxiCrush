using System.Windows;
using MaxiCrush.AdminViewControl.ViewModels;

namespace MaxiCrush.AdminViewControl
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainVM)
        {
            InitializeComponent();

            mainVM.SetAttachedWindow(this);
            DataContext = mainVM;
        }
   }
}
