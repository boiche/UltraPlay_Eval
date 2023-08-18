using System.Threading.Channels;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation.QueueService
{
    public class QueueService : IQueueService
    {
        private readonly Channel<Func<CancellationToken, BaseEntity>> _hiddenElementsQueue;
        private readonly Channel<Func<CancellationToken, BaseEntity>> _outdatedElementsQueue;

        public QueueService(int capacity)
        {
            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _hiddenElementsQueue = Channel.CreateBounded<Func<CancellationToken, BaseEntity>>(options);
            _outdatedElementsQueue = Channel.CreateBounded<Func<CancellationToken, BaseEntity>>(options);
        }

        public async ValueTask<Func<CancellationToken, BaseEntity>> DequeueHiddenAsync(CancellationToken cancellationToken)
        {
            return await _hiddenElementsQueue.Reader.ReadAsync(cancellationToken);
        }

        public async ValueTask<Func<CancellationToken, BaseEntity>> DequeueOutdatedAsync(CancellationToken cancellationToken)
        {
            return await _outdatedElementsQueue.Reader.ReadAsync(cancellationToken);
        }

        public async ValueTask QueueHiddenAsync(Func<CancellationToken, BaseEntity> workItem)
        {
            await _hiddenElementsQueue.Writer.WriteAsync(workItem);
        }

        public async ValueTask QueueOutdatedAsync(Func<CancellationToken, BaseEntity> workItem)
        {
            await _outdatedElementsQueue.Writer.WriteAsync(workItem);
        }
    }
}
