using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Messaging;

namespace Data.Repositories.Interfaces
{
    public interface IStoredEventRepository
    {
        void UpdateProcessedAt(StoredEvent storedEvent);
        Task StoreRange(List<StoredEvent> storedEvents);
        Task<IList<StoredEvent>> GetByAggregateId(Guid aggregateId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<StoredEvent>> FetchUnprocessed(int batchSize, CancellationToken cancellationToken);
    }
}