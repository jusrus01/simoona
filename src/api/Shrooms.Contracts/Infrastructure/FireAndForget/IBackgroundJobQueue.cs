namespace Shrooms.Contracts.Infrastructure.FireAndForget
{
    public interface IBackgroundJobQueue
    {
        void EnqueueJob(IBackgroundJob job);

        bool TryDequeueJob(out IBackgroundJob job);
    }
}
