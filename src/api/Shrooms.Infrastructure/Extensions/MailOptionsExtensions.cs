using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Options;

namespace Shrooms.Infrastructure.Extensions
{
    public static class MailOptionsExtensions
    {
        public static bool UseSmtp4Dev(this MailOptions options)
        {
            return options.Host == MailConstants.DefaultHost && options.Port == MailConstants.DefaultPort;
        }
    }
}
