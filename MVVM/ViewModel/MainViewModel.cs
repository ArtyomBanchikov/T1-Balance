using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using T1Balance.Commands;
using T1Balance.Core;
using T1Balance.Services;
using T1Balance.State.Authenticators;
using T1Balance.State.Navigators;

namespace T1Balance.MVVM.ViewModel
{
    class MainViewModel : ViewModelBase
    {
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

        public MainViewModel(INavigator navigator)
        {
            UpdateCurrentViewModel = new UpdateCurrentViewModelCommandAsync(ServiceProviderFactory.ServiceProvider.GetRequiredService<INavigator>(),
                ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthenticator>());
            Navigator = navigator;
        }
    }
}
