using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSC.Core.Models
{
    [Table("TicketsAssign")]
    public class TicketAssign
    {
        [Required]
        public int TicketId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime AssignDate { get; set; }
        public Ticket Ticket { get; set; }
        public Group Group { get; set; }
    }
}