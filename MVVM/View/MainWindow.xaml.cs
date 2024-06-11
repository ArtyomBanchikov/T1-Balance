using System;
using System.ComponentModel;
using System.Windows;
using Forms = System.Windows.Forms;
using T1Balance.MVVM.ViewModel;
using T1Balance.Services;
using T1Balance.State.Authenticators;
using T1Balance.State.Navigators;
using Microsoft.Extensions.DependencyInjection;

namespace T1Balance.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Forms.NotifyIcon TrayIcon { get; set; }
        private bool isClosable = false;
        private Forms.MenuItem dynamicMenuItem;
        public MainWindow()
        {
            InitializeComponent();
            IAuthenticator authenticator = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthenticator>();
            CreateTrayIcon(authenticator.CurrentAccount.Balance);
            DataContext = new MainViewModel(ServiceProviderFactory.ServiceProvider.GetRequiredService<INavigator>(), authenticator, this);
        }

        private void CreateTrayIcon(double initBalance)
        {
            TrayIcon = new Forms.NotifyIcon();
            TrayIcon.Visible = false;
            TrayIcon.Icon = new System.Drawing.Icon("Resources/Icons/sign-document-icon.ico");
            TrayIcon.Text = "T1-Balance";
            Forms.ContextMenu menu = new Forms.ContextMenu();
            string balance = initBalance.ToString("F3");
            string balanceStr = "Баланс: " + balance.Remove(balance.Length - 1) + "р";
            dynamicMenuItem = new Forms.MenuItem(balanceStr);
            dynamicMenuItem.Enabled = false;

            Forms.MenuItem menuItem2 = new Forms.MenuItem("Открыть приложение", ApplicationOpen);
            Forms.MenuItem menuItem3 = new Forms.MenuItem("Зыкрыть приложение", ApplicationExit);

            menu.MenuItems.Add(dynamicMenuItem);
            menu.MenuItems.Add("-");
            menu.MenuItems.Add(menuItem2);
            menu.MenuItems.Add(menuItem3);

            TrayIcon.ContextMenu = menu;
            TrayIcon.MouseClick += NotifyMenuOpen;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (isClosable)
            {
                TrayIcon.Dispose();
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
                TrayIcon.Visible = true;
                Hide();
            }
        }
        public void UpdateBalanceMenu(string balance)
        {
            dynamicMenuItem.Text = balance;
        }
        private void NotifyMenuOpen(object sender, Forms.MouseEventArgs e)
        {
            if (e.Button == Forms.MouseButtons.Right)
            {
                
            }
            else if (e.Button == Forms.MouseButtons.Left)
            {
                Show();
                Activate();
                TrayIcon.Visible = false;
            }
        }
        private void ApplicationOpen(object sender, EventArgs e)
        {
            Show();
            TrayIcon.Visible = false;
            Activate();
        }

        private void ApplicationExit(object sender, EventArgs e)
        {
            isClosable = true;
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            isClosable = true;
            Close();
        }
    }
}
