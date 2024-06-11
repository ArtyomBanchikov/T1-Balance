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
        private string _balance;
        public string Balance
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
            string balance = authenticator.CurrentAccount.Balance.ToString("F3");
            string balanceStr = balance.Remove(balance.Length - 1) + "р";
            Balance = balanceStr;
            Name = authenticator.CurrentUser.Name;
            TariffName = authenticator.CurrentTariff.Name;
        }
    }
}
