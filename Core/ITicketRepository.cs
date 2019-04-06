using System.Threading.Tasks;
using TSC.Core.Models;

namespace TSC.Core
{
    public interface ITicketRepository: IRepository<Ticket>
    {
         Task<QueryResult<Ticket>> GetTickets(ModelQuery queryObj);
    }
}