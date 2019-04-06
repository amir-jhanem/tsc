using System.Threading.Tasks;
using TSC.Controllers.Resources;
using TSC.Core.Models;

namespace TSC.Core
{
    public interface ITicketRepository: IRepository<Ticket>
    {
         Task<QueryResult<Ticket>> GetTickets(ModelQuery queryObj);
         void AssignTicket(AssignTicketResource assignTicketResource);
    }
}