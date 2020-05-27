using Data.Repositories;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InversionOfControl
{
    public static class DependencyInjection
    {
        public static void RegisterInjections(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<MusicLibraryDBContext>(option =>
                 option.UseSqlServer(configuration.GetConnectionString("MusicLibraryDBContext")));
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupRepository, GroupRepository>();

        }

        public static void RegisterDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MusicLibraryDBContext>(option =>
                 option.UseSqlServer(configuration.GetConnectionString("MusicLibraryDBContext")));
            services.AddScoped<IGroupRepository, GroupRepository>();
        }
    }
}
