using Dotnet_backend.Models;

namespace Dotnet_backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}