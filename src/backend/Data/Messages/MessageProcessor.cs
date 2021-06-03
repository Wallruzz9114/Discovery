using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Data.Messages
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger<MessageProcessor> _logger;

        public MessageProcessor(IAppUnitOfWork appUnitOfWork, IMessagePublisher messagePublisher, ILogger<MessageProcessor> logger)
        {
            _logger = logger;
            _messagePublisher = messagePublisher;
            _appUnitOfWork = appUnitOfWork;
        }

        public async Task ProcessMessage(int batchSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching message...");

            var messages = await _appUnitOfWork.StoredEventRepository.FetchUnprocessed(batchSize, cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    message.SetProcessedAt(DateTime.UtcNow);
                    _appUnitOfWork.StoredEventRepository.UpdateProcessedAt(message);
                    await _appUnitOfWork.CommitAsync(cancellationToken);
                    await _messagePublisher.Publish(message, cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(
                        exception,
                        $"An error has occured while processing message: { message.Id }: { exception.Message }"
                    );
                }
            }
        }
    }
}