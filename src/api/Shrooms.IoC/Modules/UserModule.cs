using Autofac;
using Shrooms.Domain.Services.Users;
using Shrooms.Domain.ServiceValidators.Validators.Users;

namespace Shrooms.IoC.Modules
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserManagerValidator>()
                .As<IApplicationUserManagerValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationSignInManagerValidator>()
                .As<IApplicationSignInManagerValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationSignInManager>()
                .As<IApplicationSignInManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>()
                .As<IApplicationUserManager>()
                .InstancePerLifetimeScope();
        }
    }
}
