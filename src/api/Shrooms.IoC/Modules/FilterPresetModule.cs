using Autofac;
using Shrooms.Domain.Services.FilterPresets;
using Shrooms.Domain.ServiceValidators.Validators.FilterPresets;
using Shrooms.Infrastructure.Interceptors;

namespace Shrooms.IoC.Modules
{
    public class FilterPresetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilterPresetService>()
                .As<IFilterPresetService>()
                .InstancePerLifetimeScope()
                .EnableInterfaceTelemetryInterceptor();

            builder.RegisterType<FilterPresetValidator>()
                .As<IFilterPresetValidator>()
                .InstancePerLifetimeScope();
        }
    }
}
