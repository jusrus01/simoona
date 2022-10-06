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
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}