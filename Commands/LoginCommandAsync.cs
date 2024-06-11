using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using T1Balance.MVVM.ViewModel;
using T1Balance.Services.XmlServices;
using T1Balance.State.Authenticators;

namespace T1Balance.Commands
{
    public class LoginCommandAsync : AsyncBaseCommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IXmlService _xmlProvider;
        private readonly LoginViewModel _loginVM;

        public LoginCommandAsync(IAuthenticator authenticator, LoginViewModel loginVM, IXmlService xmlProvider)
        {
            _authenticator = authenticator;
            _loginVM = loginVM;
            _xmlProvider = xmlProvider;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;
            if (string.IsNullOrEmpty(_loginVM.Login))
            {
                MessageBox.Show("Введите логин");
            }
            else if(string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
            }
            else
            {
                await _authenticator.Login(_loginVM.Login, password);
                if (_loginVM.IsRemember)
                {
                    _xmlProvider.IsRemember = true;
                    _xmlProvider.Token = _authenticator.CurrentUser.Token;
                }
                else if (_xmlProvider.IsRemember)
                {
                    _xmlProvider.IsRemember = false;
                    _xmlProvider.Token = "";
                }
                _xmlProvider.LastLogin = _loginVM.Login;
                _loginVM.OnLoginSuccessful();
            }
        }
    }
}
