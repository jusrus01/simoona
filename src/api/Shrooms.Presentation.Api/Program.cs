//using Autofac;
//using Autofac.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Builder;
//using Shrooms.Presentation.Api;

//var builder = WebApplication.CreateBuilder(args);
//var startup = new Startup(builder.Configuration);

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);
//builder.Host.ConfigureServices(startup.ConfigureServices);

//var app = builder.Build();

//startup.Configure(app, builder.Environment);
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shrooms.Contracts.Constants;
using System;
using System.Configuration;

namespace Shrooms.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // TODO: Remove this but pass in api url in setting
            var apiHostingUrl = ConfigurationManager.AppSettings[WebApiConstants.ConfigurationApiUrlKey];

            if (apiHostingUrl == null)
            {
                throw new Exception("Hosting url is not specified in app.config");
            }

            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSetting(WebApiConstants.ConfigurationApiUrlKey, apiHostingUrl);
                    webBuilder.UseUrls(apiHostingUrl);
                });
        }
    }
}