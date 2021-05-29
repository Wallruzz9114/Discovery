using System;
using Models.Messaging;

namespace Models.Events.Orders
{
    public class OrderPlacedEvent : Event
    {
        public OrderPlacedEvent(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            AggregateId = OrderId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}