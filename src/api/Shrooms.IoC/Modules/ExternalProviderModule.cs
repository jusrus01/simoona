using Autofac;
using Shrooms.Domain.Services.AuthenticationStates;
using Shrooms.Domain.Services.ExternalProviders;
using Shrooms.Domain.ServiceValidators.Validators.ExternalProviders;

namespace Shrooms.IoC.Modules
{
    public class ExternalProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationStateService>()
                .As<IAuthenticationStateService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalProviderValidator>()
                .As<IExternalProviderValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalProviderContext>()
                .As<IExternalProviderContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalProviderService>()
                .As<IExternalProviderService>()
                .InstancePerLifetimeScope();
        }
    }
}
