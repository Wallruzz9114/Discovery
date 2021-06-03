using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Messaging;
using Newtonsoft.Json;

namespace Data.Messages
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ILogger<MessagePublisher> _logger;
        private readonly IMediator _mediator;

        public MessagePublisher(ILogger<MessagePublisher> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public Task Publish(StoredEvent message, CancellationToken cancellationToken)
        {
            var messageType = GetType(message.MessageType);
            var entityEvent = JsonConvert.DeserializeObject(message.Payload, messageType);

            if ((message is not null) && (entityEvent is not null))
            {
                _mediator.Publish(entityEvent, cancellationToken);
                _logger.LogInformation($"Message: { message.Id } processed.");
            }

            return Task.CompletedTask;
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type is not null) return type;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type is not null) return type;
            }

            return null;
        }
    }
}