using Autofac;
using Shrooms.Domain.Services.Email.Kudos;
using Shrooms.Domain.Services.Kudos;
using Shrooms.Domain.ServiceValidators.Validators.Kudos;
using Shrooms.Infrastructure.Interceptors;

namespace Shrooms.IoC.Modules
{
    public class KudosModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KudosServiceValidator>().As<IKudosServiceValidator>().InstancePerLifetimeScope();
            //builder.RegisterType<KudosService>().As<IKudosService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<KudosExportService>().As<IKudosExportService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<KudosNotificationService>().As<IKudosNotificationService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
        }
    }
}