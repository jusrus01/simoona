using System;
using System.Threading.Tasks;

namespace Shrooms.Contracts.Infrastructure.FireAndForget
{
    public interface IFireAndForgetScheduler
    {
        void EnqueueJob<TService>(Func<TService, Task> serviceMethod);
    }
}
