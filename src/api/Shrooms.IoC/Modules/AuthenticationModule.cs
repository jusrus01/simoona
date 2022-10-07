using Autofac;
using Shrooms.Authentification.Handlers;
using Shrooms.Domain.Services.Permissions;

namespace Shrooms.IoC.Modules
{
    public class AuthenticationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PermissionService>()
                .As<IPermissionService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BasicAuthenticationHandler>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
