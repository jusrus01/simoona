using Autofac;
using Shrooms.Domain.Services.Email.InternalProviders;
using Shrooms.Domain.Services.InternalProviders;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Domain.ServiceValidators.Validators.InternalProviders;

namespace Shrooms.IoC.Modules
{
    public class InternalProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InternalProviderNotificationService>()
                .As<IInternalProviderNotificationService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InternalProviderValidator>()
                .As<IInternalProviderValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InternalProviderService>()
                .As<IInternalProviderService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TokenService>()
                .As<ITokenService>()
                .InstancePerLifetimeScope();
        }
    }
}
