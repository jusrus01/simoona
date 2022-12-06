using System;
using System.Linq;

namespace Shrooms.Authentication.External.Strategies
{
    public abstract class ExternalProviderStrategyBase
    {
        protected static bool ExcludeFromFactorySearch() => false;

        protected static T MapArgumentsToRequiredArgument<T>(object[] arguments, params Type[] types) where T : class
        {
            var requiredArguments = GetArguments(arguments, types);
            return (T)Activator.CreateInstance(typeof(T), requiredArguments);
        }

        protected static ExternalProviderPartialResult Complete(ExternalProviderResult result) => new (result);

        protected static ExternalProviderPartialResult Next<T>(params object[] arguments) where T : class => new (typeof(T), arguments);

        private static object[] GetArguments(object[] arguments, params Type[] types)
        {
            var requiredArguments = new object[types.Length];

            for (var i = 0; i < types.Length; i++)
            {
                // Calling Single() for all arguments to keep the same order that type parameters were passed in
                requiredArguments[i] = GetArgument(arguments, types[i]);
            }

            return requiredArguments;
        }

        private static object GetArgument(object[] arguments, Type type) =>
            arguments.Single(argument => argument?.GetType() == type);
    }
}