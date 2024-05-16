using Microsoft.AspNetCore.SignalR.Client;
using Notification.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using T1Balance.Core;
using T1Balance.MVVM.Model;
using T1Balance.State.Authenticators;

namespace T1Balance.MVVM.ViewModel
{
    public class InfoViewModel : ViewModelBase
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
        public IAuthenticator Authenticator { get; set; }
        public InfoViewModel(IAuthenticator authenticator)
        {
            Balance = authenticator.CurrentAccount.Balance;
            Name = authenticator.CurrentUser.Name;
            TariffName = authenticator.CurrentTariff.Name;
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
                if(account.Balance <= 0)
                    __NotificationManager.Show("Баланс отрицательный");
                else if(account.Balance > Balance)
                    __NotificationManager.Show("Пополнение баланса");
                Balance = account.Balance;
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
