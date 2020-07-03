using backend.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace backend.Data.Models
{
    public class User : IdentityUser
    {
        internal readonly string userName;

        public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
