using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using T1Balance.MVVM.ViewModel;
using T1Balance.Services;
using T1Balance.State.Navigators;

namespace T1Balance.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Visible = true;
            trayIcon.Icon = new System.Drawing.Icon("Resources/Icons/sign-document-icon.ico");
            trayIcon.Text = "T1-Balance";
            DataContext = new MainViewModel(ServiceProviderFactory.ServiceProvider.GetRequiredService<INavigator>());
        }
    }
}
