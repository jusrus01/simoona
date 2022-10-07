using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shrooms.Contracts.DAL;
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

            // Registering TenantNameContainer with pipeline that sets correct tenant name
            // based on current request
            // TODO: refactor
            builder.Register(context =>
            {
                var httpContextAccessor = context.Resolve<IHttpContextAccessor>();
                var tenantName = httpContextAccessor.HttpContext.Request.Headers["Organization"];

                return new TenantNameContainer(tenantName);
            })
            .As<ITenantNameContainer>()
            .InstancePerLifetimeScope();

            //builder.RegisterType<TenantNameContainer>()
            //    .As<ITenantNameContainer>()
            //    .ConfigurePipeline(p =>
            //    {
            //        p.Use(PipelinePhase.RegistrationPipelineStart, (context, next) =>
            //        {
            //            var httpContextAccessor = context.Resolve<IHttpContextAccessor>();

            //            // TODO: refactor
            //            var tenantNameFromHeader = httpContextAccessor.HttpContext.Request.Headers["Organization"];
            //            context.Instance = new TenantNameContainer(tenantNameFromHeader);

            //            next(context);
            //        });
            //    });

            builder.Register(contextBuilder =>
            {
                // Middleware sets TenantContainer with appropriate organization name
                var tenantContainer = contextBuilder.Resolve<ITenantNameContainer>();

                if (tenantContainer.TenantName == null)
                {
                    throw new Exception("Organization name was not provided for this request");
                }

                var configuration = contextBuilder.Resolve<IConfiguration>();
                var connectionString = configuration.GetConnectionString(tenantContainer.TenantName);

                if (connectionString == null)
                {
                    throw new Exception("Invalid organization");
                }

                var serviceProvider = contextBuilder.Resolve<IServiceProvider>();

                var optionsBuilder = new DbContextOptionsBuilder<ShroomsDbContext>()
                                   .UseApplicationServiceProvider(serviceProvider)
                                   .UseSqlServer(
                                       connectionString,
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
        }
    }
}
