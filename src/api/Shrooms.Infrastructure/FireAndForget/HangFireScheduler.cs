using System;
using System.Linq.Expressions;
using Shrooms.Contracts.Infrastructure;

namespace Shrooms.Infrastructure.FireAndForget
{
    public class HangFireScheduler : IJobScheduler
    {
        public void Enqueue<T>(Expression<Action<T>> method)
        {
            Hangfire.BackgroundJob.Enqueue(method);
        }
    }
}