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
    }
}