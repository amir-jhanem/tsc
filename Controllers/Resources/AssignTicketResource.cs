using System;
using System.ComponentModel.DataAnnotations;

namespace TSC.Controllers.Resources
{
    public class AssignTicketResource
    {
        [Required]
        public int TicketId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public bool Status { get; set; }
        public bool IsRemoved { get; set; } = false;
    }
}