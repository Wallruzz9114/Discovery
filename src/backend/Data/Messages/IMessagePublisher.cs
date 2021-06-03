using System.Threading.Tasks;
using Models.Messaging;

namespace Data.Messages
{
    public interface IMessagePublisher
    {
        Task Publish(StoredEvent message, System.Threading.CancellationToken cancellationToken);
    }
}