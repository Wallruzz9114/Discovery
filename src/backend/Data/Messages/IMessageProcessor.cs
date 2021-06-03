using System.Threading;
using System.Threading.Tasks;

namespace Data.Messages
{
    public interface IMessageProcessor
    {
        Task ProcessMessage(int batchSize, CancellationToken cancellationToken);
    }
}