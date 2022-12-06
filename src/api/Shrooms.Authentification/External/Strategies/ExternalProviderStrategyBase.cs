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

        /// <summary>
        /// Retrievie specified arguments by type
        /// </summary>
        /// <param name="arguments">All available arguments</param>
        /// <param name="types">Required types</param>
        /// <returns>Arguments returned in order that types are defined</returns>
        private static object[] GetArguments(object[] arguments, params Type[] types)
        {
            var requiredArguments = new object[types.Length];

            for (var i = 0; i < types.Length; i++)
            {
                // Calling Single() for all arguments, so order would be kept
                requiredArguments[i] = GetArgument(arguments, types[i]);
            }

            return requiredArguments;
        }

        private static object GetArgument(object[] arguments, Type type) =>
            arguments.Single(argument => argument?.GetType() == type);
    }
}