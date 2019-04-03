using System.ComponentModel.DataAnnotations;

namespace TSC.Controllers.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(255)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}