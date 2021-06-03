using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Messaging;

namespace Data.Repositories.Implementations
{
    public class StoredEventRepository : IStoredEventRepository
    {
        private readonly AppDbContext _dbContext;

        public StoredEventRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyCollection<StoredEvent>> FetchUnprocessed(int batchSize, CancellationToken cancellationToken)
        {
            var results = await _dbContext.StoredEvents
                .Where(e => e.ProcessedAt == null)
                .OrderBy(e => e.CreatedAt)
                .Take(batchSize)
                .ToListAsync(cancellationToken);

            return results;
        }

        public async Task<IList<StoredEvent>> GetByAggregateId(Guid aggregateId, CancellationToken cancellationToken)
        {
            var results = await _dbContext.StoredEvents
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.CreatedAt)
                .ToListAsync(cancellationToken);

            return results.ToImmutableArray();
        }

        public async Task StoreRange(List<StoredEvent> storedEvents)
        {
            await _dbContext.StoredEvents.AddRangeAsync(storedEvents);
        }

        public void UpdateProcessedAt(StoredEvent storedEvent)
        {
            _dbContext.StoredEvents.Update(storedEvent);
        }
    }
}