using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Role:IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
