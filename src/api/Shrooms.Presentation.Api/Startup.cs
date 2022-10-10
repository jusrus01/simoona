//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Shrooms.Presentation.Api.Configurations;
//using Microsoft.Extensions.DependencyInjection;
//using Autofac;
//using Microsoft.Extensions.Hosting;
//using Shrooms.IoC;
//using Shrooms.DataLayer.EntityModels.Models;
//using Shrooms.DataLayer.DAL;
//using Autofac.Extensions.DependencyInjection;

//namespace Shrooms.Presentation.Api
//{
//    public class Startup
//    {
//        public IConfiguration Configuration { get; }

//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public void ConfigureContainer(HostBuilderContext hostBuilderContext, ContainerBuilder builder)
//        {
//            builder.RegisterModule(new IocBootstrapperModule(Configuration));
//        }

//        public void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
//        {
//            services.AddIdentity<ApplicationUser, ApplicationRole>()
//                .AddEntityFrameworkStores<ShroomsDbContext>();

//            services.AddControllers();
//        }

//        public void Configure(WebApplication app, IWebHostEnvironment env)
//        {
//            //app.UseHttpsRedirection();
//            //app.UseStaticFiles();
//            //app.UseRouting();
//            //app.UseAuthorization();

//            app.MapControllerRoutes();

//            app.Run();
//        }
//    }
//}
using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shrooms.Authentification.Handlers;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.IoC;
using Shrooms.Presentation.Api.Configurations;
using Shrooms.Presentation.Api.GeneralCode.SerializationIgnorer;

namespace Shrooms.Presentation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new IocBootstrapperModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Telemetry.Configure(Configuration);
            SerializationIgnoreConfigs.Configure();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ShroomsDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationConstants.BasicScheme, null)
            .AddJwtBearer();

            // .AspNet.Cookie? automatically?
            // https://learn.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-6.0 
            services.AddAuthorization(options =>
            {
                options.AddBearerAsDefault();
                options.AddBasicPolicy();
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoutes();
            });
        }
    }
}