using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Messaging;

namespace Data.Repositories.Interfaces
{
    public interface IEventRepository
    {
        void UpdateProcessedAt(StoredEvent message);
        Task StoreRange(List<StoredEvent> messages);
        Task<IList<StoredEvent>> GetAggregateId(Guid aggregateId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<StoredEvent>> FetchUnprocessed(int batchSize, CancellationToken cancellationToken);
    }
}