using System;

namespace Models.Messaging
{
    public class StoredEvent : Event
    {
        protected StoredEvent() { }

        public StoredEvent(Event @event, string payload)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Payload = payload;
        }

        public void SetProcessedAt(DateTime date)
        {
            ProcessedAt = date;
        }

        public Guid Id { get; set; }
        public string Payload { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}