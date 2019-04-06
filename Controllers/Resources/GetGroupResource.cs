namespace TSC.Controllers.Resources
{
    public class GetGroupResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMembers { get; set; }
        public int TotalAssignTickets { get; set; }
    }
}