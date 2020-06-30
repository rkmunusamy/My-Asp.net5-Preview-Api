using backend.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Models
{
    public class User:IdentityUser
    {
        public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
