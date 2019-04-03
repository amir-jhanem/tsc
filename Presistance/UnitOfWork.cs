using System.Threading.Tasks;
using TSC.Core;

namespace TSC.Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TSCDbContext context;
        public UnitOfWork(TSCDbContext context)
        {
            this.context = context;
        }
        
        public Task CompleteAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}