
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User : IdentityUser<int>
    {
        public long Matricula { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
