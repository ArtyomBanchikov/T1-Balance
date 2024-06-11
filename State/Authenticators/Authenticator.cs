using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T1Balance.Core;
using T1Balance.MVVM.Model;
using T1Balance.MVVM.Model.Client;
using T1Balance.Services.WebServices.Interfaces;
using T1Balance.Services.WebServices;

namespace T1Balance.State.Authenticators
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private LoginClientModel _currentUser;
        private AccountModel _currentAccount;
        private TariffModel _currentTariff;
        private readonly WebService _webClient;
        private readonly IAccountTariffWebService _accountTariffClient;
        private readonly IModelWebService<AccountModel> _accountClient;
        private readonly IModelWebService<TariffModel> _tariffClient;
        public LoginClientModel CurrentUser
        {
            get
            {
                return _currentUser;
            }
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        public AccountModel CurrentAccount
        {
            get { return _currentAccount; }
            set
            {
                _currentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
            }
        }
        public TariffModel CurrentTariff
        {
            get { return _currentTariff; }
            set
            {
                _currentTariff = value;
                OnPropertyChanged(nameof(CurrentTariff));
            }
        }
        public bool IsLoggedIn => CurrentUser != null;

        public Authenticator(WebService client, IModelWebService<AccountModel> accountClient, IModelWebService<TariffModel> tariffClient, IAccountTariffWebService accountTariffClient)
        {
            _webClient = client;
            _accountClient = accountClient;
            _tariffClient = tariffClient;
            _accountTariffClient = accountTariffClient;
        }


        public async Task Login(string username, string password)
        {
            ShortClientModel loginUser = new ShortClientModel();
            loginUser.Login = username;
            loginUser.Password = password;

            CurrentUser = await _webClient.Login(loginUser);
            CurrentAccount = await _accountClient.GetByIdAsync(CurrentUser.AccountId);
            List<AccountTariffModel> accountTariffs = (List<AccountTariffModel>)await _accountTariffClient.GetAllByAccountAsync(CurrentAccount.Id);
            if (accountTariffs != null && accountTariffs.Count > 0)
            {
                List<TariffModel> tariffs = new List<TariffModel>();
                foreach (AccountTariffModel accountTariff in accountTariffs)
                {
                    tariffs.Add(await _tariffClient.GetByIdAsync(accountTariff.TariffId));
                }
                CurrentTariff = tariffs.LastOrDefault(tariff => tariff.IsDeleted == false);
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            _webClient.Logout();
        }

        public async Task<LoginClientModel> TokenCheck(string token)
        {
            LoginClientModel loginUser = await _webClient.TokenInfo(token);
            CurrentUser = loginUser;
            return loginUser;
        }
    }
}
