using System;
using Models.Messaging;

namespace Models.Events.Orders
{
    public class OrderChangedEvent : Event
    {
        public OrderChangedEvent(Guid orderId)
        {
            OrderId = orderId;
            AggregateId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}