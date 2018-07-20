using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SimpleWebApp.Models;

namespace SimpleWebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SimpleWebAppUser class
    public class SimpleWebAppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
