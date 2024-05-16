using System.Threading.Tasks;
using T1Balance.MVVM.Model.Client;
using T1Balance.MVVM.Model;

namespace T1Balance.State.Authenticators
{
    public interface IAuthenticator
    {
        LoginClientModel CurrentUser { get; }
        AccountModel CurrentAccount { get; set; }
        TariffModel CurrentTariff { get; set; }
        bool IsLoggedIn { get; }
        Task<bool> Login(string username, string password);
        void Logout();
        Task<LoginClientModel> TokenCheck(string token);
    }
}
