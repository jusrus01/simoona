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

        public IFireAndForgetJob DequeueJob()//TODO: clean up before commit
        {
            if (_jobs.TryDequeue(out IFireAndForgetJob job))
            {
                return job;
            }

            return null;
        }
    }
}
