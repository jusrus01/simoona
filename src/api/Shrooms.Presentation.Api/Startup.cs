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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shrooms.Authentification.Handlers;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.IoC;
using Shrooms.Presentation.Api.Configurations;
using Shrooms.Presentation.Api.GeneralCode.SerializationIgnorer;
using System;
using System.Text;

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
            var applicationOptions = services.AddOptions(Configuration);

            // TODO: change this
            services.AddCors(options => 
                options.AddDefaultPolicy(builder => 
                    builder.AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(s => true)));

            Telemetry.Configure(Configuration);
            SerializationIgnoreConfigs.Configure();

            // Need to auth... with roles

            // TODO: figure out google login
            // and using it along side bearer


            //decisions that need to be made:
            //signing key jwt
            //refresh token

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<ShroomsDbContext>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = ".AspNet.Cookies22";
            //});
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationConstants.BasicScheme, null)
            .AddCookie(options =>
            {
                options.Cookie.Name = ".AspNet.Cookies";
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = applicationOptions.ApiUrl,
                    ValidAudience = applicationOptions.ApiUrl,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationOptions.Authentication.Jwt.Key))
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId = applicationOptions.Authentication.Google.ClientId;
                options.ClientSecret = applicationOptions.Authentication.Google.ClientSecret;
                options.SaveTokens = false;
                options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
            });

            services.AddAuthorization(options =>
            {
                options.AddBearerAsDefault();
                options.AddBasicPolicy();
                options.AddStoragePolicy();
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoutes();
            });
        }
    }
}