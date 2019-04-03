using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSC.Core.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Subject { get; set; }
        [Required]
        [StringLength(255)]
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        // Contact Info
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactEmail { get; set; }
    }
}