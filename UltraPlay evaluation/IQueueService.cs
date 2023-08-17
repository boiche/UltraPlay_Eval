using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation
{
    public interface IQueueService
    {
        ValueTask QueueHiddenAsync(Func<CancellationToken, BaseEntity> workItem);

        ValueTask<Func<CancellationToken, BaseEntity>> DequeueHiddenAsync(CancellationToken cancellationToken);
        ValueTask QueueOutdatedAsync(Func<CancellationToken, BaseEntity> workItem);

        ValueTask<Func<CancellationToken, BaseEntity>> DequeueOutdatedAsync(CancellationToken cancellationToken);
    }
}