using Autofac;
using Shrooms.Authentication.External;
using Shrooms.Authentication.External.Strategies;
using Shrooms.Domain.Services.AuthenticationStates;
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
                .As<IExternalProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalLoginRedirectToProviderStrategy>()
                .As<IExternalProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalRegisterRedirectToProviderStrategy>()
                .As<IExternalProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalLoginStrategy>()
                .As<IExternalProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalPictureService>()
                .As<IExternalPictureService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalRegisterStrategy>()
                .As<IExternalProviderStrategy>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExternalRestoreStrategy>()
                .As<IExternalProviderStrategy>()
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
