using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MusicLibraryApplication.Areas.Identity.IdentityHostingStartup))]
namespace MusicLibraryApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}