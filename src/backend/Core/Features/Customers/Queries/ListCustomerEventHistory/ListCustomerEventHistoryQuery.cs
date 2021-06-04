using System;
using System.Collections.Generic;
using Core.Base;
using Core.Base.Queries;

namespace Core.Features.Customers.Queries.ListCustomerEventHistory
{
    public class ListCustomerEventHistoryQuery : IQuery<IList<CustomerHistoryData>>
    {
        public ListCustomerEventHistoryQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}