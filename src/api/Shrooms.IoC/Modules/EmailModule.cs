using Autofac;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using Shrooms.Infrastructure.Email;
using Shrooms.Infrastructure.Email.Cache;
using Shrooms.Infrastructure.Extensions;
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
                // TODO: Figure out why IOptions<MailSettings> does not get serialized
                var options = context.Resolve<IOptions<ApplicationOptions>>().Value;

                if (options.MailSettings.UseSmtp4Dev)
                {
                    return new Smpt4DevMailService(Options.Create(options.MailSettings));
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
