using System;
using Models.Messaging;

namespace Models.Events.Customers
{
    public class CustomerCreatedEvent : Event
    {
        public CustomerCreatedEvent(Guid customerId, string name, string email)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            AggregateId = CustomerId;
        }

        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}