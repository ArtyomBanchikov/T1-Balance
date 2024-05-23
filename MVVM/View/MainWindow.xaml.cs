using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        NotifyIcon trayIcon;
        private bool isClosable = false;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(ServiceProviderFactory.ServiceProvider.GetRequiredService<INavigator>());
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (isClosable)
            {
                trayIcon.Dispose();
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
                trayIcon = new NotifyIcon();
                trayIcon.Visible = true;
                trayIcon.Icon = new System.Drawing.Icon("Resources/Icons/sign-document-icon.ico");
                trayIcon.Text = "T1-Balance";
                trayIcon.ContextMenuStrip = new ContextMenuStrip();
                trayIcon.ContextMenuStrip.Items.Add("Открыть", null, ApplicationOpen);
                trayIcon.ContextMenuStrip.Items.Add("Закрыть", null, ApplicationExit);
                trayIcon.MouseClick += NotifyMenuOpen;
                Hide();
            }
        }

        private void NotifyMenuOpen(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                trayIcon.ContextMenuStrip.Show();
            else if (e.Button == MouseButtons.Left)
            {
                Show();
                Activate();
                trayIcon.Dispose();
            }
        }
        private void ApplicationOpen(object sender, EventArgs e)
        {
            Show();
            trayIcon.Dispose();
        }

        private void ApplicationExit(object sender, EventArgs e)
        {
            isClosable = true;
            Close();
        }
    }
}
