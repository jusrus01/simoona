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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shrooms.Authentification.Handlers;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Infrastructure.FireAndForget;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Infrastructure.FireAndForget;
using Shrooms.Infrastructure.Logger;
using Shrooms.IoC;
using Shrooms.Presentation.Api.BackgroundServices;
using Shrooms.Presentation.Api.Configurations;
using Shrooms.Presentation.Api.GeneralCode.SerializationIgnorer;
using Shrooms.Presentation.Api.GlobalFilters;
using Shrooms.Presentation.Api.Middlewares;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }
        
        public ApplicationOptions? ApplicationOptions { get; set; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new IocBootstrapperModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationOptions = services.AddOptions(Configuration, Environment);

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
                // Temporary settings, for development
                // TODO: Remove before PR
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Activate email confirmation
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<ShroomsDbContext>()
            .AddDefaultTokenProviders();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationConstants.BasicScheme, null)
            .AddCookie(options =>
            {
                options.Cookie.Name = ".AspNet.Cookies";
                // TODO: Match cookie expiration date to bearer token
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
                options.Events.OnCreatingTicket = (context) =>
                {
                    var picture = context.User.GetProperty(WebApiConstants.ClaimPicture).GetString();

                    if (picture == null)
                    {
                        return Task.CompletedTask;
                    }

                    context.Identity.AddClaim(new Claim(WebApiConstants.ClaimPicture, picture));

                    return Task.CompletedTask;
                };
            });

            // Some of the registration has to be done here,
            // because Shrooms.IoC does not have reference to
            // Shrooms.Presentation.Api services e.g. background jobs
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IFireAndForgetJobQueue, FireAndForgetJobQueue>();
            services.AddHostedService<FireAndForgetBackgroundService>();

            services.AddAuthorization(options =>
            {
                options.AddBearerAsDefault();
                options.AddBasicPolicy();
                options.AddStoragePolicy();
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalModelStateValidationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseMiddleware<MultiTenancyMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoutes();
            });
        }
    }
}