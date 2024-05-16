using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using T1Balance.MVVM.Model;
using T1Balance.Services.WebServices.Interfaces;

namespace T1Balance.Services.WebServices
{
    public class AccountWebService : IModelWebService<AccountModel>
    {
        private readonly WebService _webService;

        public AccountWebService(WebService webService)
        {
            _webService = webService;
        }

        public async Task<IEnumerable<AccountModel>> GetAllAsync()
        {
            return await _webService.Client.GetFromJsonAsync<IEnumerable<AccountModel>>("/account");
        }

        public async Task<AccountModel> GetByIdAsync(int id)
        {
            return await _webService.Client.GetFromJsonAsync<AccountModel>($"/account/{id}");
        }
    }
}
