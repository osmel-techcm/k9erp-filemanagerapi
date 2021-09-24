﻿using Microsoft.AspNetCore.Identity;

namespace fileManagerInfrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int IdGroup { get; set; }
        public bool? masterDealer { get; set; }
    }
}
