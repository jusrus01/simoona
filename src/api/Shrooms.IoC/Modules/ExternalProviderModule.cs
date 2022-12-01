using Autofac;
using Autofac.Diagnostics;
using Shrooms.Domain.Services.AuthenticationStates;
using Shrooms.Domain.Services.ExternalProviders;
using Shrooms.Domain.Services.ExternalProviders.Strategies;
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

            builder.RegisterType<ExternalProviderLinkAccountStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalLoginRedirectToProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalRegisterRedirectToProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalLoginStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalRegisterStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalProviderStrategyFactory>()
                .As<IExternalProviderStrategyFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalProviderService>()
                .As<IExternalProviderService>()
                .InstancePerLifetimeScope();
        }
    }
}
