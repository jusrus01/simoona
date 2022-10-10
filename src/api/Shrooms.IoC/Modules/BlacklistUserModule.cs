using Autofac;
using Shrooms.Domain.Services.BlacklistUsers;
using Shrooms.Domain.ServiceValidators.Validators.BlacklistUsers;

namespace Shrooms.IoC.Modules
{
    public class BlacklistUserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlacklistService>()
                .As<IBlacklistService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BlacklistValidator>()
                .As<IBlacklistValidator>()
                .InstancePerLifetimeScope();
        }
    }
}
