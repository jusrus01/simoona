namespace Shrooms.Contracts.Infrastructure.FireAndForget
{
    public interface IFireAndForgetJobQueue
    {
        void EnqueueJob(IFireAndForgetJob job);

        IFireAndForgetJob DequeueJob();
    }
}
