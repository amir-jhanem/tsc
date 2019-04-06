using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSC.Core.Models
{
    [Table("UserGroups")]
    public class UserGroup
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public Group  Group { get; set; }
    }
}