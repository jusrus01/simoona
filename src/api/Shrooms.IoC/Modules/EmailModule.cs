using Autofac;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using Shrooms.Infrastructure.Email;
using Shrooms.Infrastructure.Email.Cache;
using Shrooms.Infrastructure.Interceptors;
using System;

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

            builder.Register(context =>
            {
                var options = context.Resolve<IOptions<MailOptions>>();

                if (options.Value.UseSmtp4Dev)
                {
                    return new Smpt4DevMailService(options);
                }
                else
                {
                    throw new NotImplementedException();
                }
            })
            .As<IMailService>()
            .InstancePerLifetimeScope()
            .EnableInterfaceTelemetryInterceptor();
        }
    }
}
