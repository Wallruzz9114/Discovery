using System.Collections.Generic;
using Models.Messaging;
using Newtonsoft.Json;

namespace Core.Base
{
    public abstract class EventNormalizer<THistoryData> where THistoryData : HistoryData, new()
    {
        public abstract IList<THistoryData> ToHistoryData(IList<StoredEvent> messages);

        protected IList<THistoryData> GetHistoryData(IList<StoredEvent> messages)
        {
            IList<THistoryData> historyData = new List<THistoryData>();

            foreach (var message in messages)
            {
                THistoryData history = JsonConvert.DeserializeObject<THistoryData>(message.Payload);
                history.Id = message.Id.ToString();
                history.Timestamp = message.CreatedAt.ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                if (message.MessageType.Contains("Created"))
                    history.Action = "Created";

                if (message.MessageType.Contains("Updated"))
                    history.Action = "Updated";

                if (message.MessageType.Contains("Deleted"))
                    history.Action = "Deleted";

                historyData.Add(history);
            }

            return historyData;
        }
    }
}