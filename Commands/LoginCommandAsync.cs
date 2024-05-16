using System.Threading.Tasks;
using T1Balance.MVVM.ViewModel;
using T1Balance.State.Authenticators;

namespace T1Balance.Commands
{
    public class LoginCommandAsync : AsyncBaseCommand
    {
        private readonly IAuthenticator _authenticator;
        public LoginViewModel _loginVM;

        public LoginCommandAsync(IAuthenticator authenticator, LoginViewModel loginVM)
        {
            _authenticator = authenticator;
            _loginVM = loginVM;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            bool succes = await _authenticator.Login(_loginVM.Login, parameter.ToString());
        }
    }
}
