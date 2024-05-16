using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using T1Balance.MVVM.Model.Client;

namespace T1Balance.Services.WebServices
{
    public class WebService
    {
        public HttpClient Client { get; }
        public string Token { get; }

        public WebService()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            Client = new HttpClient(handler);
            Client.BaseAddress = new Uri("");
        }

        public async Task<LoginClientModel> Login(ShortClientModel user)
        {
            //var response = Client.PostAsJsonAsync($"/login", user).Result;
            //LoginUserModel loginUser = response.Content.ReadFromJsonAsync<LoginUserModel>().Result;
            LoginClientModel loginUser = await Client.PostAsJsonAsync("/login", user).Result.Content.ReadFromJsonAsync<LoginClientModel>();
            if (loginUser != null &&
                loginUser.Name != null &&
                loginUser.Token != null)
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginUser.Token);
            else
                throw new Exception("Ошибка авторизации");
            return loginUser;
        }

        public void Logout()
        {
            Client.DefaultRequestHeaders.Clear();
        }

        public async Task<LoginClientModel> TokenInfo(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            LoginClientModel user = Client.GetFromJsonAsync<LoginClientModel>($"/userinfo/").Result;
            return user;
        }
    }
}
