using System.Threading.Tasks;
using Simply_Watered.Models;

namespace Simply_Watered.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateJWTTokenAsync(ApplicationUser user);
    }
}
