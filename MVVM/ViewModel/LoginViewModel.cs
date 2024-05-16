using System.Windows.Input;
using T1Balance.Commands;
using T1Balance.Core;
using T1Balance.Services.XmlServices;
using T1Balance.State.Authenticators;

namespace T1Balance.MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private bool isRemember;
        public bool IsRemember
        {
            get { return isRemember; }
            set
            {
                isRemember = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, IXmlService xmlProvider)
        {
            if (!string.IsNullOrEmpty(xmlProvider.LastLogin))
                Login = xmlProvider.LastLogin;
            LoginCommand = new LoginCommandAsync(authenticator, this);
        }
    }
}
