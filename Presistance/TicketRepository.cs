using TSC.Core;
using TSC.Core.Models;

namespace TSC.Presistance
{
    public class TicketRepository : Repository<Ticket>,ITicketRepository
    {

        public TicketRepository(TSCDbContext Context):base(Context)
        {

        }
    }
}