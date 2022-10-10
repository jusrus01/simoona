using Autofac;
using Shrooms.Domain.Services.Birthday;
using Shrooms.Domain.Services.Wall;
using Shrooms.Domain.Services.Wall.Posts;
using Shrooms.Domain.Services.Wall.Posts.Comments;
using Shrooms.Domain.ServiceValidators.Validators.Wall;
using Shrooms.Infrastructure.Interceptors;

namespace Shrooms.IoC.Modules
{
    public class WallModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();

            builder.RegisterType<WallService>().As<IWallService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
            builder.RegisterType<WallValidator>().As<IWallValidator>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();

            builder.RegisterType<BirthdayService>().As<IBirthdayService>().InstancePerLifetimeScope().EnableInterfaceTelemetryInterceptor();
        }
    }
}
