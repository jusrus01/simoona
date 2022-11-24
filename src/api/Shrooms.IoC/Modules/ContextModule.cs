using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Infrastructure.FireAndForget;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.DAL;
using Shrooms.Infrastructure.FireAndForget;
using System;

namespace Shrooms.IoC.Modules
{
    public class ContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .InstancePerLifetimeScope();

            // Registering TenantNameContainer that sets correct tenant name based on current request
            builder.Register(context =>
            {
                var httpContextAccessor = context.Resolve<IHttpContextAccessor>();
                var tenantName = httpContextAccessor.HttpContext.Request.Headers[WebApiConstants.OrganizationHeader];

                return new TenantNameContainer(tenantName);
            })
            .As<ITenantNameContainer>()
            .InstancePerLifetimeScope();

            builder.Register(contextBuilder =>
            {
                var tenantContainer = contextBuilder.Resolve<ITenantNameContainer>();
                var applicationOptions = contextBuilder.Resolve<IOptions<ApplicationOptions>>().Value;

                var serviceProvider = contextBuilder.Resolve<IServiceProvider>();

                var optionsBuilder = new DbContextOptionsBuilder<ShroomsDbContext>()
                                   .UseApplicationServiceProvider(serviceProvider)
                                   .UseSqlServer(
                                       applicationOptions.ConnectionStrings[tenantContainer.TenantName],
                                       serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));

                return optionsBuilder.Options;
            });

            builder.Register(context => context.Resolve<DbContextOptions<ShroomsDbContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ShroomsDbContext>()
                .As<IDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ShroomsDbContext>()
                .As<ShroomsDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork2>()
                .As<IUnitOfWork2>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FireAndForgetScheduler>()
                .As<IFireAndForgetScheduler>()
                .InstancePerLifetimeScope();
        }
    }
}
