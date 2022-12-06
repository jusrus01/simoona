using System;

namespace Shrooms.Authentication.External
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
