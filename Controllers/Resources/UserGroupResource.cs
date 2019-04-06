using System.ComponentModel.DataAnnotations;

namespace TSC.Controllers.Resources
{
    public class UserGroupResource
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}