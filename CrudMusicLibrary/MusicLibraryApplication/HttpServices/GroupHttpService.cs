using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Model.Options;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

namespace MusicLibraryApplication.HttpServices
{
    public class GroupHttpService : IGroupService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<MusicLibraryHttpOptions> _musicLibraryHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;
        public GroupHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<MusicLibraryHttpOptions> musicLibraryHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _musicLibraryHttpOptions = musicLibraryHttpOptions ?? throw new ArgumentNullException(nameof(musicLibraryHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;

            _httpClient = httpClientFactory.CreateClient(musicLibraryHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_musicLibraryHttpOptions.CurrentValue.Timeout);
        }
        private async Task<bool> AddAuthJwtToRequest()
        {
            var jwtCookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("musicLibraryToken", out var jwtFromCookie);
            if (!jwtCookieExists)
            {
                await _signInManager.SignOutAsync();
                return false;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtFromCookie);
            return true;
        }
        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            var jwtSucess = await AddAuthJwtToRequest();
            if (!jwtSucess)
            {
                return null;
            }
            var httpResponseMessage = await _httpClient.GetAsync(_musicLibraryHttpOptions.CurrentValue.GroupPath);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return null;
            }
            return JsonConvert.DeserializeObject<List<GroupEntity>>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{ _musicLibraryHttpOptions.CurrentValue.GroupPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);
            
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return null;
            }
            return JsonConvert.DeserializeObject<GroupEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }

            var uriPath = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}/{updatedEntity.GroupId}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
      
        public async Task<bool> CheckMascotAsync(string mascot, int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return false; //rever, pode ser que tenha que inverter ou levantar uma exceção
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}/CheckMascot/{mascot}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return false; //rever, pode ser que tenha que inverter ou levantar uma exceção
            }

            return bool.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }
}
