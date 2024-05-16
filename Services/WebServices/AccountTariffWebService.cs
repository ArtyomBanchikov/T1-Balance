using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using T1Balance.MVVM.Model;
using T1Balance.Services.WebServices.Interfaces;

namespace T1Balance.Services.WebServices
{
    public class AccountTariffWebService : IAccountTariffWebService
    {
        private readonly WebService _webService;

        public AccountTariffWebService(WebService webService)
        {
            _webService = webService;
        }

        public async Task<IEnumerable<AccountTariffModel>> GetAllAsync()
        {
            return await _webService.Client.GetFromJsonAsync<IEnumerable<AccountTariffModel>>("/accounttariff");
        }

        public async Task<IEnumerable<AccountTariffModel>> GetAllByAccountAsync(int id)
        {
            return await _webService.Client.GetFromJsonAsync<IEnumerable<AccountTariffModel>>($"/accounttariff/{id}");
        }
    }
}
