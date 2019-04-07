using System.Threading.Tasks;
using TSC.Controllers.Resources;
using TSC.Core.Models;

namespace TSC.Core
{
    public interface IGroupRepository:IRepository<Group>
    {
        Task<GetGroupResource> GetGroup(int id);
        Task<QueryResult<GetGroupResource>> GetGroups(ModelQuery queryObj);
        void Add(SaveGroupResource saveGroupResource);
        void Update(SaveGroupResource saveGroupResource);
    }
}