using Autofac;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Notifications;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Picture;
using Shrooms.Domain.Services.UserService;
using Shrooms.Domain.Services.VacationPages;
using Shrooms.Domain.ServiceValidators.Validators.Organizations;
using Shrooms.Domain.ServiceValidators.Validators.Pictures;
using Shrooms.Infrastructure.Interceptors;

namespace Shrooms.IoC.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CookieService>().As<ICookieService>().InstancePerLifetimeScope();

            builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerLifetimeScope().EnableClassTelemetryInterceptor();
            builder.RegisterType<OrganizationValidator>().As<IOrganizationValidator>().InstancePerLifetimeScope();

            builder.RegisterType<PictureValidator>().As<IPictureValidator>().InstancePerLifetimeScope();
            builder.RegisterType<PictureContentTypeService>().As<IPictureContentTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<VacationPageService>().As<IVacationPageService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
        }
    }
}