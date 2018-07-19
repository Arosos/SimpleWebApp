﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SimpleWebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SimpleWebAppUser class
    public class SimpleWebAppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
