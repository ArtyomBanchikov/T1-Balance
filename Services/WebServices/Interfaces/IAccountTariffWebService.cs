using System.Collections.Generic;
using System.Threading.Tasks;
using T1Balance.MVVM.Model;

namespace T1Balance.Services.WebServices.Interfaces
{
    public interface IAccountTariffWebService
    {
        Task<IEnumerable<AccountTariffModel>> GetAllAsync();
        Task<IEnumerable<AccountTariffModel>> GetAllByAccountAsync(int id);
    }
}
