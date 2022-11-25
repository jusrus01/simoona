using Autofac;
using Shrooms.Infrastructure.Interceptors;
using Shrooms.IoC.Modules;

namespace Shrooms.IoC
{
    public class IocBootstrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AssemblyModule());
            builder.Register(_ => new TelemetryLoggingInterceptor());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ContextModule());
            builder.RegisterModule(new RoleModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new AuthenticationModule());
            builder.RegisterModule(new UserModule());
            builder.RegisterModule(new EmailModule());
            builder.RegisterModule(new MapperModule());
            builder.RegisterModule(new InternalProviderModule());
            builder.RegisterModule(new WallModule());
            builder.RegisterModule(new KudosModule());
            builder.RegisterModule(new KudosBasketModule());
            builder.RegisterModule(new WebHookCallbacksModule());
            builder.RegisterModule(new RefreshTokenModule());
            builder.RegisterModule(new ExternalLinksModule());
            builder.RegisterModule(new SupportModule());
            builder.RegisterModule(new AdministrationUsers());
            builder.RegisterModule(new JobModule());
            builder.RegisterModule(new FilterPresetModule());
            builder.RegisterModule(new BlacklistUserModule());
            builder.RegisterModule(new EmployeeModule());
            builder.RegisterModule(new ExternalProviderModule());
        }
    }
}
