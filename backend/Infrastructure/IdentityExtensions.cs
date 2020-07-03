using System.Linq;
using System.Security.Claims;

namespace backend.Infrastructure
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user
            .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;
    }
}
