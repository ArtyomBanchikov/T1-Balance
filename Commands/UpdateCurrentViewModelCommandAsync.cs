using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using T1Balance.MVVM.ViewModel;
using T1Balance.Services;
using T1Balance.State.Authenticators;
using T1Balance.State.Navigators;

namespace T1Balance.Commands
{
    public class UpdateCurrentViewModelCommandAsync : AsyncBaseCommand
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;

        public UpdateCurrentViewModelCommandAsync(INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Info:
                        {
                            InfoViewModel viewModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<InfoViewModel>();

                            _navigator.CurrentViewModel = viewModel;
                            break;
                        }
                    case ViewType.Settings:
                        {
                            _navigator.CurrentViewModel = new SettingsViewModel();
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
