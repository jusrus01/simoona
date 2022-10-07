using Autofac;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Infrastructure.Email;
using Shrooms.Infrastructure.Email.Cache;
using Shrooms.Infrastructure.Interceptors;

namespace Shrooms.IoC.Modules
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailTemplateConfiguration>()
                .As<IEmailTemplateConfiguration>()
                .SingleInstance();

            builder.Register(context =>
            {
                var emailTemplateConfiguration = context.Resolve<IEmailTemplateConfiguration>();
                var mailTemplateCache = new MailTemplateCache();
                var emailTemplateCompiler = new EmailTemplateCompiler(mailTemplateCache, emailTemplateConfiguration);

                emailTemplateCompiler.Register();

                return mailTemplateCache;
            })
            .As<IMailTemplateCache>()
            .SingleInstance();

            builder.RegisterType<MailingService>()
                .As<IMailingService>()
                .InstancePerLifetimeScope()
                .EnableInterfaceTelemetryInterceptor();
        }
    }
}
