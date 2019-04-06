using System.Threading.Tasks;
using TSC.Controllers.Resources;
using TSC.Core.Models;

namespace TSC.Core
{
    public interface IGroupRepository:IRepository<Group>
    {
        Task<QueryResult<GetGroupResource>> GetGroups(ModelQuery queryObj);
        void MemberGroup(UserGroupResource userGroupResource);
    }
}