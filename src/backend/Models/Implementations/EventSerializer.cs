using System;
using System.Text.Json;
using Models.Interfaces;
using Models.Messaging;

namespace Models.Implementations
{
    public class EventSerializer : IEventSerializer
    {
        public string Serialize<TE>(TE @event) where TE : Event
        {
            if (@event is null) throw new ArgumentNullException(nameof(@event));
            return JsonSerializer.Serialize(@event, @event.GetType());
        }
    }
}