using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Shrooms.Authentification.Handlers;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Permissions;
using Shrooms.Domain.Services.Roles;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Infrastructure.Configuration;
using Shrooms.Infrastructure.CustomCache;
using Shrooms.Infrastructure.FireAndForget;
using Shrooms.Infrastructure.Interceptors;
using Shrooms.IoC.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Shrooms.IoC
{
    // Executes once after a request is made (and configs execute based on lifetime...)
    public class IocBootstrapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var shroomsApi = Assembly.Load("Shrooms.Presentation.Api");
            var dataLayer = Assembly.Load("Shrooms.DataLayer");

            var modelMappings = Assembly.Load("Shrooms.Presentation.ModelMappings");

            builder.RegisterAssemblyTypes(dataLayer);
            builder.RegisterAssemblyTypes(modelMappings).AssignableTo(typeof(Profile)).As<Profile>();
            builder.RegisterAssemblyTypes(shroomsApi).Where(t => typeof(IBackgroundWorker).IsAssignableFrom(t)).InstancePerDependency().AsSelf();
            
            builder.RegisterType<AsyncRunner>().As<IAsyncRunner>().SingleInstance();

            builder.Register(_ => new TelemetryLoggingInterceptor());

            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ContextModule());
            builder.RegisterModule(new RoleModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new AuthenticationModule());
            builder.RegisterModule(new EmailModule());
            builder.RegisterModule(new MapperModule());

            builder.RegisterModule(new WallModule());
            builder.RegisterModule(new KudosModule());
            builder.RegisterModule(new KudosBasketModule());
            builder.RegisterModule(new WebHookCallbacksModule());
            builder.RegisterModule(new RefreshTokenModule());
            builder.RegisterModule(new ExternalLinksModule());
            builder.RegisterModule(new SupportModule());
            builder.RegisterModule(new AdministrationUsers());
            builder.RegisterModule(new JobModule());
            builder.RegisterModule(new FilterPresetModule());
            builder.RegisterModule(new BlacklistUserModule());
            builder.RegisterModule(new EmployeeModule());

            // TODO: move
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        }

        //public static IContainer Bootstrap(IAppBuilder app, Func<string> getConnectionStringName, HttpConfiguration config)
        //{
        //    var builder = new ContainerBuilder();
        //    var shroomsApi = Assembly.Load("Shrooms.Presentation.Api");
        //    var dataLayer = Assembly.Load("Shrooms.DataLayer");
        //    var modelMappings = Assembly.Load("Shrooms.Presentation.ModelMappings");

        //    RegisterIdentityDbContext(builder, getConnectionStringName);

        //    var settings = new JsonSerializerSettings();
        //    settings.ContractResolver = new SignalRContractResolver();
        //    var serializer = JsonSerializer.Create(settings);
        //    builder.RegisterInstance(serializer).As<JsonSerializer>();
        //    builder.RegisterHubs(shroomsApi);

        //    builder.RegisterAssemblyTypes(shroomsApi).Where(t => typeof(IBackgroundWorker).IsAssignableFrom(t)).InstancePerDependency().AsSelf();
        //    builder.RegisterType<AsyncRunner>().As<IAsyncRunner>().SingleInstance();

        //    // Email templates
        //    builder.RegisterType<MailTemplateCache>().As<IMailTemplateCache>().SingleInstance();
        //    builder.RegisterType<EmailTemplateConfiguration>().As<IEmailTemplateConfiguration>().SingleInstance();

        //    builder.RegisterWebApiModelBinderProvider();
        //    builder.RegisterWebApiFilterProvider(config);
        //    builder.RegisterAssemblyTypes(dataLayer);
        //    builder.RegisterAssemblyTypes(modelMappings).AssignableTo(typeof(Profile)).As<Profile>();

        //    // Interceptor
        //    builder.Register(_ => new TelemetryLoggingInterceptor());

        //    builder.RegisterType(typeof(UnitOfWork2)).As(typeof(IUnitOfWork2)).InstancePerRequest();
        //    builder.RegisterType(typeof(EfUnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
        //    builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));

        //    // Authorization types
        //    builder.RegisterType<MailingService>().As<IMailingService>().InstancePerRequest().EnableInterfaceTelemetryInterceptor();
        //    builder.RegisterType<PostNotificationService>().As<IPostNotificationService>().InstancePerRequest().EnableInterfaceTelemetryInterceptor();
        //    builder.RegisterType<CommentNotificationService>().As<ICommentNotificationService>().InstancePerRequest().EnableInterfaceTelemetryInterceptor();
        //    builder.Register(_ => app.GetDataProtectionProvider()).InstancePerRequest();
        //    builder.RegisterType<PermissionService>().As<IPermissionService>().PropertiesAutowired().InstancePerRequest();
        //    builder.RegisterType<SyncTokenService>().As<ISyncTokenService>().InstancePerRequest().EnableInterfaceTelemetryInterceptor();
        //    builder.RegisterType<UserAdministrationValidator>().As<IUserAdministrationValidator>().InstancePerRequest();
        //    builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerRequest().EnableInterfaceTelemetryInterceptor();
        //    builder.RegisterType<ProjectsService>().As<IProjectsService>().InstancePerRequest().EnableInterfaceTelemetryInterceptor();

        //    builder.RegisterModule(new IdentityModule());
        //    builder.RegisterModule(new ServicesModule());
        //    builder.RegisterModule(new InfrastructureModule());
        //    builder.RegisterModule(new WallModule());
        //    builder.RegisterModule(new KudosModule());
        //    builder.RegisterModule(new KudosBasketModule());
        //    builder.RegisterModule(new WebHookCallbacksModule());
        //    builder.RegisterModule(new RefreshTokenModule());
        //    builder.RegisterModule(new ExternalLinksModule());
        //    builder.RegisterModule(new RoleModule());
        //    builder.RegisterModule(new SupportModule());
        //    builder.RegisterModule(new AdministrationUsers());
        //    builder.RegisterModule(new JobModule());
        //    builder.RegisterModule(new FilterPresetModule());
        //    builder.RegisterModule(new BlacklistUserModule());
        //    builder.RegisterModule(new EmployeeModule());

        //    builder.RegisterApiControllers(shroomsApi);

        //    //RegisterExtensions(builder, new Logger());
        //    RegisterMapper(builder);

        //    var container = builder.Build();
        //    GlobalConfiguration.Configuration.UseAutofacActivator(container);

        //    return container;
        //}

        //private static void RegisterExtensions(ContainerBuilder builder, ILogger logger)
        //{
        //    var extensionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions");

        //    if (!Directory.Exists(extensionsPath))
        //    {
        //        logger.Error(new DirectoryNotFoundException("Extension directory does not exist"));

        //        return;
        //    }

        //    var files = Directory.GetFiles(extensionsPath, "*.dll", SearchOption.AllDirectories);

        //    foreach (var dll in files)
        //    {
        //        try
        //        {
        //            var assembly = Assembly.LoadFrom(dll);

        //            builder.RegisterAssemblyTypes(assembly);
        //            builder.RegisterAssemblyTypes(assembly).AssignableTo(typeof(Profile)).As<Profile>();
        //            builder.RegisterAssemblyModules(assembly);
        //            builder.RegisterApiControllers(assembly);
        //        }
        //        catch (FileLoadException loadException)
        //        {
        //            logger.Error(loadException);
        //        }
        //        catch (BadImageFormatException formatException)
        //        {
        //            logger.Error(formatException);
        //        }
        //        catch (ArgumentNullException nullException)
        //        {
        //            logger.Error(nullException);
        //        }
        //    }

        //    // Needed for Hangfire to process jobs from extension assemblies
        //    AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
        //    {
        //        var assemblyName = new AssemblyName(args.Name);
        //        var existing = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(c => c.FullName == assemblyName.FullName);

        //        if (existing != null)
        //        {
        //            return existing;
        //        }

        //        return null;
        //    };
        //}
    }
}
