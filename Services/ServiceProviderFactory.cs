using T1Balance.MVVM.Model.Client;
using T1Balance.MVVM.Model;
using T1Balance.Services.WebServices.Interfaces;
using T1Balance.Services.WebServices;
using T1Balance.State.Authenticators;
using Microsoft.Extensions.DependencyInjection;
using System;
using T1Balance.Services.XmlServices;
using T1Balance.State.Navigators;
using T1Balance.MVVM.ViewModel;

namespace T1Balance.Services
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }
        static ServiceProviderFactory()
        {
            IServiceCollection services = new ServiceCollection();


            services.AddSingleton<IXmlService, XmlService>();

            services.AddSingleton<WebService>();

            services.AddScoped<IModelWebService<AccountModel>, AccountWebService>();
            services.AddScoped<IModelWebService<ClientModel>, ClientWebService>();
            services.AddScoped<IModelWebService<TariffModel>, TariffWebService>();
            services.AddScoped<IAccountTariffWebService, AccountTariffWebService>();

            services.AddSingleton<InfoViewModel>();
            services.AddSingleton<SettingsViewModel>();

            services.AddSingleton<INavigator, Navigator>();

            services.AddSingleton<IAuthenticator, Authenticator>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
