using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using T1Balance.MVVM.Model;
using T1Balance.Services.WebServices.Interfaces;

namespace T1Balance.Services.WebServices
{
    public class TariffWebService : IModelWebService<TariffModel>
    {
        private readonly WebService _webService;

        public TariffWebService(WebService webService)
        {
            _webService = webService;
        }

        public async Task<IEnumerable<TariffModel>> GetAllAsync()
        {
            return await _webService.Client.GetFromJsonAsync<IEnumerable<TariffModel>>("/tariff");
        }

        public async Task<TariffModel> GetByIdAsync(int id)
        {
            return await _webService.Client.GetFromJsonAsync<TariffModel>($"/tariff/{id}");
        }
    }
}
