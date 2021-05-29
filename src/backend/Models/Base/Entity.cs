using System;
using System.Collections.Generic;
using Models.Messaging;

namespace Models.Base
{
    public abstract class Entity
    {
        private List<Event> _entityEvents;

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public IReadOnlyCollection<Event> EntityEvents => _entityEvents?.AsReadOnly();

        public void AddEntityEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            _entityEvents ??= new List<Event>();
            _entityEvents.Add(@event);
        }
        public void ClearEntityEvents()
        {
            _entityEvents.Clear();
        }
    }
}