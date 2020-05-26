using Crosscutting.Identity.RequestModels;
using System.Threading.Tasks;

namespace MusicLibraryApplication.HttpServices
{
    public interface IAuthHttpService
    {
        Task<string> GetTokenAsync(LoginRequest loginRequest);
    }
}
