using Microsoft.AspNetCore.Identity;

namespace Dotnet_backend.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}