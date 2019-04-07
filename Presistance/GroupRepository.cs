using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<ApplicationUser> _userManager;

        public GroupRepository(TSCDbContext context,
        UserManager<ApplicationUser> userManager) : base(context)
        {
            this.context = context;
            _userManager = userManager;

        }
        public async Task<GetGroupResource> GetGroup(int id)
        {
            var getGroupResource = new GetGroupResource();
            var group = await context.Set<Group>().FindAsync(id);
            var usersGroup = context.Set<UserGroup>().Where(ug=>ug.GroupId == id).ToList();
            getGroupResource.Id = group.Id;
            getGroupResource.Name = group.Name;
            getGroupResource.TotalMembers = usersGroup.Count;

            foreach(var item in usersGroup)
            {
                var user = await _userManager.Users.Where(u=>u.Id == item.UserId).FirstOrDefaultAsync();
                
                getGroupResource.Members.Add(new GetMemberResource{
                    FullName = user.FullName,
                    UserName = user.UserName
                });
            }
            return getGroupResource;
        }
        public void Add(SaveGroupResource saveGroupResource){

            var group = new Group {Name = saveGroupResource.Name};

            var users =   _userManager.Users.ToList();

            var selectedUsers =  users.Where(u=> saveGroupResource.Members.Any(m=>m == u.UserName) ).ToList();

            context.Groups.Add(group);

            foreach(var item in selectedUsers){
                var userGroup = new UserGroup {GroupId = group.Id,UserId = item.Id};
                context.UserGroups.Add(userGroup);
            }
        }
        public void Update(SaveGroupResource saveGroupResource){

            var group = context.Groups.FindAsync(saveGroupResource.Id).Result;

            var users =   _userManager.Users.ToList();
            group.Name = saveGroupResource.Name;

            var removedUsers = users.Where(u => !saveGroupResource.Members.Any(gr=>gr == u.UserName)).Select(u=> new UserGroup{GroupId = saveGroupResource.Id , UserId = u.Id  }).ToList();
                // context.UserGroups.RemoveRange(removedUsers);

            // var addedUsers = users.Where(u => saveGroupResource.Members.Any(gr=>gr == u.UserName)).Select(u=> new UserGroup{GroupId = saveGroupResource.Id , UserId = u.Id }).ToList();
            
            // foreach (var auser in addedUsers)
            //     context.UserGroups.Add(auser);

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
        
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}