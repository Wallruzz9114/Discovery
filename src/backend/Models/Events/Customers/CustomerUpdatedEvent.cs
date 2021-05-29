using System;
using Models.Messaging;

namespace Models.Events.Customers
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
            AggregateId = CustomerId;
        }

        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
    }
}