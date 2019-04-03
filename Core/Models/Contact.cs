using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSC.Core.Models
{
    [Table("Contacts")]
    public class Contact
    {
        public int Id { get; set; }

    }
}