using Autofac;
using Shrooms.Domain.Services.WebHookCallbacks;
using Shrooms.Domain.Services.WebHookCallbacks.BirthdayNotification;
using Shrooms.Domain.Services.WebHookCallbacks.BlacklistUsers;
using Shrooms.Domain.Services.WebHookCallbacks.UserAnonymization;
using Shrooms.Infrastructure.Interceptors;

namespace Shrooms.IoC.Modules
{
    public class WebHookCallbacksModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BirthdaysNotificationWebHookService>().As<IBirthdaysNotificationWebHookService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<UsersAnonymizationWebHookService>().As<IUsersAnonymizationWebHookService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<BlacklistUserStatusChangeWebHookService>().As<IBlacklistUserStatusChangeWebHookService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();

            builder.RegisterType<WebHookCallbackServices>().As<IWebHookCallbackServices>().InstancePerLifetimeScope().PropertiesAutowired().EnableInterfaceTelemetryInterceptor();
        }
    }
}