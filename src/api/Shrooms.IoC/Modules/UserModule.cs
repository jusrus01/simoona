using Autofac;
using Shrooms.Domain.Services.Users;
using Shrooms.Domain.ServiceValidators.Validators.Users;

namespace Shrooms.IoC.Modules
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManagerValidator>()
                .As<IUserManagerValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>()
                .As<IApplicationUserManager>()
                .InstancePerLifetimeScope();
        }
    }
}
