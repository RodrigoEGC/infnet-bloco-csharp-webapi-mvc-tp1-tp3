using Crosscutting.Identity.RequestModels;
using Domain.Model.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibraryApplication.HttpServices
{
    public class AuthHttpService : IAuthHttpService
    {
        private readonly HttpClient _httpClient;

        public AuthHttpService(
           IHttpClientFactory httpClientFactory,
           IOptionsMonitor<MusicLibraryHttpOptions> musicLibraryHttpOptions)
        {
            _httpClient = httpClientFactory.CreateClient(musicLibraryHttpOptions.CurrentValue.Name);
        }

        private class TokenResponse
        {
            public string token { get; set; }
        }
        public async Task<string> GetTokenAsync(LoginRequest loginRequest)
        {
            var uriPath = $"api/Auth/Token";

            var httpContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var content = httpResponseMessage.Content;
            var jsonToken = await content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonToken);

            return tokenResponse.token;
        }
    }
}
