using AutoMapper;
using TSC.Controllers.Resources;
using TSC.Core.Models;

namespace TSC.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Ticket, TicketResource>()
                .ForMember(tr => tr.Contact, opt =>opt.MapFrom(t => new ContactResource{ Name = t.ContactName,Email = t.ContactEmail}));

            // API Resource to Domain
            CreateMap<QueryResource, ModelQuery>();
            CreateMap<TicketResource,Ticket>()
                .ForMember(t=>t.Id,opt =>opt.Ignore())
                .ForMember(t=>t.ContactName,opt =>opt.MapFrom(tr=>tr.Contact.Name))
                .ForMember(t=>t.ContactEmail,opt =>opt.MapFrom(tr=>tr.Contact.Email));
        }
    }
}