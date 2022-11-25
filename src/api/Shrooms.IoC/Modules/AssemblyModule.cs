using Autofac;
using AutoMapper;
using Shrooms.Contracts.Infrastructure;
using System.Reflection;

namespace Shrooms.IoC.Modules
{
    public class AssemblyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var shroomsApi = Assembly.Load("Shrooms.Presentation.Api");
            var dataLayer = Assembly.Load("Shrooms.DataLayer");
            var modelMappings = Assembly.Load("Shrooms.Presentation.ModelMappings");

            builder.RegisterAssemblyTypes(dataLayer);
            builder.RegisterAssemblyTypes(modelMappings)
                .AssignableTo(typeof(Profile))
                .As<Profile>();

            builder.RegisterAssemblyTypes(shroomsApi)
                .Where(t => typeof(IBackgroundWorker)
                .IsAssignableFrom(t))
                .InstancePerDependency()
                .AsSelf();

        }
    }
}
