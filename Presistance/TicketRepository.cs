using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSC.Core;
using TSC.Core.Models;
using TSC.Extensions;

namespace TSC.Presistance
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly TSCDbContext context;
        public TicketRepository(TSCDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<QueryResult<Ticket>> GetTickets(ModelQuery queryObj)
        {
            var result = new QueryResult<Ticket>();

            var query = context.Tickets.AsQueryable();
        
            var columnsMap = new Dictionary<string, Expression<Func<Ticket, object>>>()
            {
                ["date"] = t => t.CreationDate
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}