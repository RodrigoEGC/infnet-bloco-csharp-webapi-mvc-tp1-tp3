using Domain.Model.Interfaces.Services;
using Domain.Model.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicLibraryApplication.HttpServices;

namespace MusicLibraryApplication.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var musicLibraryHttpOptionsSection = configuration.GetSection(nameof(MusicLibraryHttpOptions));
            var musicLibraryHttpOptions = musicLibraryHttpOptionsSection.Get<MusicLibraryHttpOptions>();

            services.AddHttpClient(musicLibraryHttpOptions.Name, x => { x.BaseAddress = musicLibraryHttpOptions.ApiBaseUrl; });

            services.AddScoped<IGroupService, GroupHttpService>();
        }
    }
}
