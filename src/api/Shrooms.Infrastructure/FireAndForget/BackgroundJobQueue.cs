using Shrooms.Contracts.Infrastructure.FireAndForget;
using System.Collections.Concurrent;

namespace Shrooms.Infrastructure.FireAndForget
{
    public class BackgroundJobQueue : IBackgroundJobQueue
    {
        private readonly ConcurrentQueue<IBackgroundJob> _jobs = new();

        public void EnqueueJob(IBackgroundJob job)
        {
            _jobs.Enqueue(job);
        }

        public bool TryDequeueJob(out IBackgroundJob job)
        {
            return _jobs.TryDequeue(out job);
        }
    }
}
