using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;

namespace TSC.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        [StringLength(255)]
        public string FullName { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}