using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SocialApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
    }
}
