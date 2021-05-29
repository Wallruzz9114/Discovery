using System;

namespace Models.Messaging
{
    public abstract class Message
    {
        protected Message()
        {
            MessageType = GetType().FullName;
        }

        public Guid AggregateId { get; protected set; }
        public string MessageType { get; protected set; }
    }
}