using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
using Shrooms.Presentation.Api.BackgroundServices;
using Shrooms.Presentation.Api.GlobalFilters;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options.AddDefinedOrigins(configuration));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                if (environment.IsDevelopment())
                {
                    options.AddDevelopmentOptions();
                }
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<ShroomsDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalModelStateValidationFilter>();
            });
        }

        public static void ConfigureBackgroundJobService(this IServiceCollection services)
        {
            // Some of the registration has to be done here,
            // because Shrooms.IoC does not have reference to
            // Shrooms.Presentation.Api services e.g. background jobs
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
            services.AddHostedService<BackgroundJobsService>();
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddBearerAsDefault();
                options.AddBasicPolicy();
                options.AddStoragePolicy();
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, ApplicationOptions applicationOptions)
        {
            services.ConfigureDefaultAuthentication()
                .ConfigureBasicAuthentication()
                .ConfigureCookieAuthentication(applicationOptions.Authentication.Jwt)
                .ConfigureJwtAuthentication(applicationOptions)
                .ConfigureGoogleAuthentication(applicationOptions.Authentication.Google);
        }

        private static AuthenticationBuilder ConfigureBasicAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationConstants.BasicScheme, null);
        }

        private static AuthenticationBuilder ConfigureGoogleAuthentication(this AuthenticationBuilder builder, GoogleAuthenticationOptions googleOptions)
        {
            return builder.AddGoogle(options =>
            {
                options.ClientId = googleOptions.ClientId;
                options.ClientSecret = googleOptions.ClientSecret;
                options.SaveTokens = false;
                options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                options.Events.OnCreatingTicket = ExtractGoogleProfilePictureClaim;
            });
        }

        private static Task ExtractGoogleProfilePictureClaim(OAuthCreatingTicketContext context)
        {
            var picture = context.User.GetProperty(WebApiConstants.ClaimPicture).GetString();

            if (picture == null)
            {
                return Task.CompletedTask;
            }

            context.Identity.AddClaim(new Claim(WebApiConstants.ClaimPicture, picture));

            return Task.CompletedTask;
        }

        private static AuthenticationBuilder ConfigureJwtAuthentication(this AuthenticationBuilder builder, ApplicationOptions applicationOptions)
        {
            // TODO: Add additional implementation that uses Azure Key Vault
            return builder.AddJwtBearer(options =>
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
            });
        }

        private static AuthenticationBuilder ConfigureCookieAuthentication(this AuthenticationBuilder builder, JwtAuthenticationOptions jwtOptions)
        {
            // Cookie authentication is only used in StorageController and should not be used anywhere else.
            // We use it, because current front-end implementation is not able to send Bearer tokens when retrieving images.
            return builder.AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(jwtOptions.DurationInDays);
                options.SlidingExpiration = true;
            });
        }

        private static AuthenticationBuilder ConfigureDefaultAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }
    }
}
