
using System.ComponentModel.DataAnnotations;

namespace TSC.Controllers.Resources
{
    public class SaveGroupResource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string[] Members { get; set; }
    }
}