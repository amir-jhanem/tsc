using System.Collections.Generic;
using System.Threading.Tasks;

namespace TSC.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}