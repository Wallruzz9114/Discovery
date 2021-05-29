using System;
using MediatR;

namespace Models.Messaging
{
    public abstract class Event : Message, INotification
    {
        public Event()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; set; }
    }
}