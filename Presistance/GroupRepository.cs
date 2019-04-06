using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly TSCDbContext context;
        public GroupRepository(TSCDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<QueryResult<GetGroupResource>> GetGroups(ModelQuery queryObj)
        {
            var result = new QueryResult<GetGroupResource>();

            var query = context.Groups.Select(g=> new GetGroupResource{
                Id = g.Id,
                Name = g.Name,
                TotalMembers = context.UserGroups.Where(ug=>ug.GroupId == g.Id).Count(),
                TotalAssignTickets = context.TicketsAssign.Where(ta=>ta.GroupId == g.Id).Count()
            }).AsQueryable();
        
            // var columnsMap = new Dictionary<string, Expression<Func<Group, object>>>()
            // {
            //     ["name"] = t => t.Name
            // };
            // query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void MemberGroup(UserGroupResource userGroupResource)
        {
            var userGroup = context.UserGroups.Where(ug=>ug.GroupId == userGroupResource.GroupId && ug.UserId == userGroupResource.UserId).FirstOrDefault();
            if(userGroup == null){
                context.UserGroups.Add(new UserGroup{GroupId = userGroupResource.GroupId,UserId = userGroupResource.UserId});
            }else{
                if(userGroupResource.IsRemoved){
                    context.UserGroups.Remove(userGroup);                    
                }
            }
            
        }
    }
}