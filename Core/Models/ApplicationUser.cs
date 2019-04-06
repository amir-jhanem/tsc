using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TSC.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        [StringLength(255)]
        public string FullName { get; set; }
    }
}