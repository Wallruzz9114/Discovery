using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using Core.Base;
using Core.Base.Queries;

namespace Core.Features.Customers.Queries.ListCustomerEventHistory
{
    public class ListCustomerEventQueryHandler : IQueryHandler<ListCustomerEventHistoryQuery, IList<CustomerHistoryData>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;

        public ListCustomerEventQueryHandler(IAppUnitOfWork appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        public async Task<IList<CustomerHistoryData>> Handle(ListCustomerEventHistoryQuery request, CancellationToken cancellationToken)
        {
            var storedEvents = await _appUnitOfWork
                .StoredEventRepository.GetByAggregateId(request.CustomerId, cancellationToken);
            var normalizer = new CustomerEventNormalizer();
            var categoryHistoryData = normalizer.ToHistoryData(storedEvents);

            return categoryHistoryData;
        }
    }
}