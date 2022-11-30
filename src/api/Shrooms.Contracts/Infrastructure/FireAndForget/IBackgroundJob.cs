using System;
using System.Threading.Tasks;

namespace Shrooms.Contracts.Infrastructure.FireAndForget
{
    public interface IBackgroundJob
    {
        Type ServiceType { get; }

        Func<object, Task> ServiceMethod { get; }

        string TenantName { get; }
    }
}