using Shrooms.Domain.Services.ExternalProviders.Strategies;
using System;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderPartialResult
    {
        public Type NextStrategy { get; }

        public ExternalProviderResult Result { get; }

        public object[] Arguments { get; }

        public bool IsComplete { get => Result != null; }

        public ExternalProviderPartialResult(Type nextStrategy, params object[] arguments)
        {
            NextStrategy = nextStrategy;
            Arguments = arguments;
        }

        public ExternalProviderPartialResult(ExternalProviderResult result)
        {
            Result = result;
        }
    }
}
