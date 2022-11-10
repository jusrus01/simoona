using Autofac;
using Shrooms.Domain.Services.ExternalProviders;

namespace Shrooms.IoC.Modules
{
    public class ExternalProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExternalProviderContext>()
                .As<IExternalProviderContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalProviderService>()
                .As<IExternalProviderService>()
                .InstancePerLifetimeScope();
        }
    }
}
