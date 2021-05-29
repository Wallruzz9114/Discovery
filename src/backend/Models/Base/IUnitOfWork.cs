using System.Threading;
using System.Threading.Tasks;

namespace Models.Base
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}