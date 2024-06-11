using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Notification.Wpf;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using T1Balance.Commands;
using T1Balance.Core;
using T1Balance.MVVM.Model;
using T1Balance.MVVM.View;
using T1Balance.Services;
using T1Balance.State.Authenticators;
using T1Balance.State.Navigators;

namespace T1Balance.MVVM.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private static readonly NotificationManager __NotificationManager = new NotificationManager();
        private HubConnection _connection;
        private double _balance;
        public double Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _tariffName;
        public string TariffName
        {
            get { return _tariffName; }
            set
            {
                _tariffName = value;
                OnPropertyChanged();
            }
        }
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModel { get; set; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator, MainWindow mainWindow)
        {
            UpdateCurrentViewModel = new UpdateCurrentViewModelCommandAsync(ServiceProviderFactory.ServiceProvider.GetRequiredService<INavigator>(),
                ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthenticator>());
            Navigator = navigator;
            InfoViewModel infoVM = ServiceProviderFactory.ServiceProvider.GetRequiredService<InfoViewModel>();
            _connection = new HubConnectionBuilder()
                .WithUrl("", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(authenticator.CurrentUser.Token);
                    //options.UseDefaultCredentials = true;
                })
                .WithAutomaticReconnect()
                .Build();

            _connection.On<AccountModel>("Receive", (account) =>
            {
                if (account.Balance <= 0)
                    __NotificationManager.Show("Баланс отрицательный");
                else if (account.Balance > authenticator.CurrentAccount.Balance)
                {
                    __NotificationManager.Show("Пополнение баланса");
                    authenticator.CurrentAccount.Balance = account.Balance;

                    string balance = account.Balance.ToString("F3");
                    string balanceStr = "Баланс: " + balance.Remove(balance.Length - 1) + "р";

                    infoVM.Balance = balanceStr;

                    mainWindow.UpdateBalanceMenu(balanceStr);

                }
            });
            try
            {
                _connection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
