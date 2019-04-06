using Microsoft.EntityFrameworkCore;
using TSC.Core.Models;

namespace TSC.Presistance
{
    public class TSCDbContext:DbContext
    {
        public TSCDbContext(DbContextOptions<TSCDbContext> options):base(options)
        {
            
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<TicketAssign> TicketsAssign { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<TicketAssign>().HasKey(ta => 
              new { ta.TicketId, ta.GroupId });

            modelBuilder.Entity<UserGroup>().HasKey(ta => 
              new { ta.UserId, ta.GroupId });
        }
    }
}