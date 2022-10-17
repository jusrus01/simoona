using Autofac;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Domain.Services.DailyMailingService;
using Shrooms.Infrastructure.CustomCache;
using Shrooms.Infrastructure.Email.Templates;
using Shrooms.Infrastructure.ExcelGenerator;
using Shrooms.Infrastructure.FireAndForget;
using Shrooms.Infrastructure.Interceptors;
using Shrooms.Infrastructure.Storage;
using Shrooms.Infrastructure.Storage.AzureBlob;
using Shrooms.Infrastructure.Storage.FileSystem;
using Shrooms.Infrastructure.SystemClock;

namespace Shrooms.IoC.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Logger>().As<ILogger>().InstancePerMatchingLifetimeScope(AutofacJobActivator.LifetimeScopeTag, MatchingScopeLifetimeTags.RequestLifetimeScopeTag);

            //builder.RegisterType<MailingService>().As<IMailingService>()
            //    .InstancePerMatchingLifetimeScope(AutofacJobActivator.LifetimeScopeTag, MatchingScopeLifetimeTags.RequestLifetimeScopeTag)
            //    .EnableInterfaceTelemetryInterceptor();

            builder.RegisterGeneric(typeof(CustomCache<,>)).As(typeof(ICustomCache<,>)).SingleInstance();

            builder.RegisterType<SystemClock>().As<ISystemClock>().SingleInstance();
            builder.RegisterType<ExcelBuilderFactory>().As<IExcelBuilderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MailTemplate>().As<IMailTemplate>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<DailyMailingService>().As<IDailyMailingService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<HangFireScheduler>().As<IJobScheduler>().InstancePerLifetimeScope();

            // TODO: Figure out
            //builder.Register<IStorage>(context =>
            //{
            //    var settings = context.Resolve<IApplicationSettings>();

            //    if (settings.StorageConnectionString != null)
            //    {
            //        return new AzureStorage(settings);
            //    }

            //    return new FileSystemStorage();
            //})
            //.InstancePerLifetimeScope();
        }
    }
}