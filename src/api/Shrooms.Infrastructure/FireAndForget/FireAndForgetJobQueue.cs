using Shrooms.Contracts.Infrastructure.FireAndForget;
using System.Collections.Concurrent;

namespace Shrooms.Infrastructure.FireAndForget
{
    public class FireAndForgetJobQueue : IFireAndForgetJobQueue
    {
        private readonly ConcurrentQueue<IFireAndForgetJob> _jobs = new();

        public void EnqueueJob(IFireAndForgetJob job)
        {
            _jobs.Enqueue(job);
        }

        public bool TryDequeueJob(out IFireAndForgetJob job)
        {
            return _jobs.TryDequeue(out job);
        }
    }
}
