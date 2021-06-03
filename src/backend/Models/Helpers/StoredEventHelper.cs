using System;
using System.Reflection;
using MediatR;
using Models.Events.Customers;
using Models.Interfaces;
using Models.Messaging;
using Newtonsoft.Json;

namespace Models.Helpers
{
    public static class StoredEventHelper
    {
        public static StoredEvent BuildFromEntityEvent<TEvent>(TEvent @event, IEventSerializer eventSerializer) where TEvent : Event
        {
            if (@event is null) throw new ArgumentNullException(nameof(@event));
            if (eventSerializer is null) throw new ArgumentNullException(nameof(eventSerializer));

            return new StoredEvent(@event, eventSerializer.Serialize(@event));
        }

        public static T Deserialize<T>(StoredEvent message) where T : class, INotification
        {
            var type = GetEventType(message.MessageType);
            return JsonConvert.DeserializeObject(message.Payload, type) as T;
        }

        public static Type GetEventType(string messageType)
        {
            Type type = Assembly.GetAssembly(typeof(CustomerCreatedEvent)).GetType(messageType);
            return type;
        }
    }
}