using System;
using System.ComponentModel.DataAnnotations;

namespace TSC.Controllers.Resources
{
    public class TicketResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Subject { get; set; }
        [Required]
        [StringLength(255)]
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public ContactResource Contact { get; set; }
    }
}