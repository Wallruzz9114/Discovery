using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Base;

namespace Data.Repositories
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        protected readonly TDbContext DbContext;

        protected UnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            await StoreEvents(cancellationToken);
            return await DbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        protected abstract Task StoreEvents(CancellationToken cancellationToken);
    }
}