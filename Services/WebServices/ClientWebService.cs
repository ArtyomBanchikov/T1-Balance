using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using T1Balance.MVVM.Model.Client;
using T1Balance.Services.WebServices.Interfaces;

namespace T1Balance.Services.WebServices
{
    public class ClientWebService : IModelWebService<ClientModel>
    {
        private readonly WebService _webService;

        public ClientWebService(WebService webService)
        {
            _webService = webService;
        }

        public async Task<IEnumerable<ClientModel>> GetAllAsync()
        {
            return await _webService.Client.GetFromJsonAsync<IEnumerable<ClientModel>>("/client");
        }

        public async Task<ClientModel> GetByIdAsync(int id)
        {
            return await _webService.Client.GetFromJsonAsync<ClientModel>($"/client/{id}");
        }
    }
}
