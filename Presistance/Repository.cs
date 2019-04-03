using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSC.Core;

namespace TSC.Presistance
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;

        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public virtual async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public virtual void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public virtual void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }
    }
}