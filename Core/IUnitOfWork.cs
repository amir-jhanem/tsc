using System.Threading.Tasks;

namespace TSC.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}