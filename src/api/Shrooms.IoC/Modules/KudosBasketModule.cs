using Autofac;
using Shrooms.Domain.ServiceValidators.Validators.KudosBaskets;

namespace Shrooms.IoC.Modules
{
    public class KudosBasketModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KudosBasketValidator>().As<IKudosBasketValidator>().InstancePerLifetimeScope();
            //builder.RegisterType<KudosBasketService>().As<IKudosBasketService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
        }
    }
}
