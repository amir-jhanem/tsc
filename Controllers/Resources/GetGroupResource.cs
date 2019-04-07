using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TSC.Controllers.Resources
{
    public class GetGroupResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMembers { get; set; }
        public int TotalAssignTickets { get; set; }
        public ICollection<GetMemberResource> Members { get; set; }
        public GetGroupResource()
        {
            Members = new Collection<GetMemberResource>();
        }
    }
}