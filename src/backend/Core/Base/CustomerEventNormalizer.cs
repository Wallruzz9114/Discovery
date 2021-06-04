using System;
using System.Collections.Generic;
using System.Linq;
using Models.Messaging;

namespace Core.Base
{
    public class CustomerEventNormalizer : EventNormalizer<CustomerHistoryData>
    {
        public override IList<CustomerHistoryData> ToHistoryData(IList<StoredEvent> messages)
        {
            var historyData = GetHistoryData(messages).OrderBy(c => c.Timestamp);
            var categoryHistory = new List<CustomerHistoryData>();
            var last = new CustomerHistoryData();

            foreach (var data in historyData)
            {
                var newData = new CustomerHistoryData()
                {
                    Id = data.Id == Guid.Empty.ToString() || data.Id == last.Id ? "" : data.Id,
                    Name = string.IsNullOrWhiteSpace(data.Name) || data.Name == last.Name ? "" : data.Name,
                    Action = string.IsNullOrWhiteSpace(data.Action) ? "" : data.Action,
                    Timestamp = data.Timestamp
                };

                categoryHistory.Add(newData);
                last = data;
            }

            return categoryHistory;
        }
    }
}