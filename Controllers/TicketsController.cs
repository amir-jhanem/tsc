using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSC.Controllers.Resources;
using TSC.Core;
using TSC.Core.Models;

namespace TSC.Controllers
{
    [Route("/api/tickets")]
    public class TicketsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITicketRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public TicketsController(IMapper mapper,ITicketRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketResource ticketResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var ticket = mapper.Map<TicketResource,Ticket>(ticketResource);
            ticket.CreationDate = DateTime.Now;

            repository.Add(ticket);
            await unitOfWork.CompleteAsync();

            ticket = await repository.Get(ticket.Id);
            var result = mapper.Map<Ticket,TicketResource>(ticket);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await repository.Get(id);

            if(ticket == null)
                return NotFound();

            repository.Remove(ticket);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await repository.Get(id);

            if(ticket == null)
                return NotFound();

            var ticketResource = mapper.Map<Ticket,TicketResource>(ticket);
            return Ok(ticketResource);
        }
        [HttpGet]
        public async Task<QueryResultResource<TicketResource>> GetTickets(TicketQueryResource filterResource)
        {
            var filter = mapper.Map<TicketQueryResource, TicketQuery>(filterResource);
            var queryResult = await repository.GetTickets(filter);

            return mapper.Map<QueryResult<Ticket>, QueryResultResource<TicketResource>>(queryResult);
        }


    }
}