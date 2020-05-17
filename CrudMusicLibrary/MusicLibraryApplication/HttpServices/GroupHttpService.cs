using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Domain.Model.Options;
using Microsoft.Extensions.Options;
using System.Text;

namespace MusicLibraryApplication.HttpServices
{
    public class GroupHttpService : IGroupService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<MusicLibraryHttpOptions> _musicLibraryHttpOptions;

        public GroupHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<MusicLibraryHttpOptions> musicLibraryHttpOptions)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _musicLibraryHttpOptions = musicLibraryHttpOptions ?? throw new ArgumentNullException(nameof(musicLibraryHttpOptions));

            _httpClient = httpClientFactory.CreateClient(musicLibraryHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_musicLibraryHttpOptions.CurrentValue.Timeout);
        }
        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            var result = await _httpClient.GetStringAsync(_musicLibraryHttpOptions.CurrentValue.GroupPath);
            return JsonConvert.DeserializeObject<List<GroupEntity>>(result);
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            var pathWithId = $"{ _musicLibraryHttpOptions.CurrentValue.GroupPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<GroupEntity>(result);
        }

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            var uriPath = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(uriPath, httpContent);
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            var pathWithId = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}/{updatedEntity.GroupId}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(pathWithId, httpContent);
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}/{id}";
            await _httpClient.DeleteAsync(pathWithId);
        }
      
        public async Task<bool> CheckMascotAsync(string mascot, int id)
        {
            var pathWithId = $"{_musicLibraryHttpOptions.CurrentValue.GroupPath}/CheckMascot/{mascot}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return bool.Parse(result);
        }
    }
}
