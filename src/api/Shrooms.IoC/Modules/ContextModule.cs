using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.DAL;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Linq;

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
                var tenantName = ExtractTenantName(httpContextAccessor.HttpContext);

                return new TenantNameContainer(tenantName);//TODO: fix lowercase name appearance
            })
            .As<ITenantNameContainer>()
            .InstancePerLifetimeScope();

            builder.Register(contextBuilder =>
            {
                var tenantContainer = contextBuilder.Resolve<ITenantNameContainer>();

                if (tenantContainer.TenantName == null)
                {
                    throw new Exception("Organization name was not provided for this request");
                }

                var applicationOptions = contextBuilder.Resolve<IOptions<ApplicationOptions>>().Value;

                if (!applicationOptions.ConnectionStrings.TryGetValue(tenantContainer.TenantName, out var connectionString))
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

        private static string ExtractTenantName(HttpContext httpContext)
        {
            if (httpContext.User != null &&
                httpContext.User.Identity.IsAuthenticated &&
                httpContext.User.Claims.Any(x => x.Type == "OrganizationName"))
            {
                return httpContext.User.Claims.First(x => x.Type == "OrganizationName").Value;
            }

            if (httpContext.Request.Headers.TryGetValue("Organization", out var organizationFromHeader))
            {
                return organizationFromHeader;
            }

            if (httpContext.Request.Query.TryGetValue("organization", out var organizationFromUri))
            {
                return organizationFromUri;
            }

            if (httpContext.Request.Path.ToString().StartsWith("/storage"))
            {
                return httpContext.Request.Path.ToString().Split('/')[2];
            }

            return null;
        }
    }
}
