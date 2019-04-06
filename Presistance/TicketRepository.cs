using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSC.Controllers.Resources;
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

        public void AssignTicket(AssignTicketResource assignTicketResource)
        {
            var TicketsAssign = context.TicketsAssign.Where(ug=> ug.TicketId == assignTicketResource.TicketId &&ug.GroupId == assignTicketResource.GroupId ).FirstOrDefault();
            if(TicketsAssign == null){
                context.TicketsAssign.Add(new TicketAssign{TicketId = assignTicketResource.TicketId,GroupId = assignTicketResource.GroupId,AssignDate = DateTime.Now , Status = assignTicketResource.Status});
            }else{

                if(assignTicketResource.IsRemoved){
                    context.TicketsAssign.Remove(TicketsAssign);                    
                }
            }
            
        }


    }
}