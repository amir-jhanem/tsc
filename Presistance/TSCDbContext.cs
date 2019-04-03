using Microsoft.EntityFrameworkCore;

namespace TSC.Presistance
{
    public class TSCDbContext:DbContext
    {
        public TSCDbContext(DbContextOptions<TSCDbContext> options):base(options)
        {
            
        }
    }
}