using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaxiCrush.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaxiCrush.AdminViewControl
{
    public partial class AddPermissionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        private readonly AddPermissionWindow _attachedWindow;
        private readonly RestClient _restClient;

        public AddPermissionViewModel(AddPermissionWindow attachedWindow, RestClient restClient)
        {
            _restClient = restClient;
            _attachedWindow = attachedWindow;
        }

        [RelayCommand]
        public async Task AddPermission()
        {
            await Utils.HandleRequest(async () =>
            {
                var permission = await _restClient.CreatePermissionAsync(Name);

                _attachedWindow.OnPermissionCreated?.Invoke();
                MessageBox.Show("Permission successfully created !");
                _attachedWindow.Close();
            });
        }
    }
    /// <summary>
    /// Logique d'interaction pour AddPermissionWindow.xaml
    /// </summary>
    public partial class AddPermissionWindow : Window
    {
        public Action? OnPermissionCreated { get; set; }

        public AddPermissionWindow()
        {
            InitializeComponent();
        }
    }
}
