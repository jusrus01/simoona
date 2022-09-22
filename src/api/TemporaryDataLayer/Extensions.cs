using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace TemporaryDataLayer
{
    internal static class Extensions
    {
        public const string IsDeleted = nameof(IsDeleted); // TODO: save somewhere

        public static PropertyBuilder AddSoftDelete<T>(this EntityTypeBuilder<T> builder, bool hasDefaultValue = false) where T : class
        {
            if (hasDefaultValue)
            {
                return builder.Property(typeof(bool), IsDeleted)
                    .HasDefaultValue(false);
            }

            return builder.Property(typeof(bool), IsDeleted);
        }

        public static void AddLikes<T>(
            this EntityTypeBuilder<T> builder,
            Expression<Func<T, LikesCollection>> likes) where T : class
        {
            builder.OwnsOne(likes)
               .Property(model => model.Serialized)
               .HasColumnName("Likes")
               .HasDefaultValue("{}");
        }

        public static void AddImages<T>(
            this EntityTypeBuilder<T> builder,
            Expression<Func<T, ImageCollection>> image) where T : class
        {
            builder.OwnsOne(image)
               .Property(model => model.Serialized)
               .HasColumnName("Images");
        }

        public static void AddDefaultBaseModelConfiguration<T>(this EntityTypeBuilder<T> builder, bool hasDefaultValue  = false) where T : BaseModel
        {
            if (hasDefaultValue)
            {
                builder.Property(model => model.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValue("1900-01-01");

                builder.Property(model => model.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValue("1900-01-01");

                return;
            }

            builder.Property(model => model.Created)
                .HasColumnType("datetime");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime");
        }

        public static void AddOrganization<T>(this EntityTypeBuilder<T> builder, DeleteBehavior deleteBehavior = DeleteBehavior.Restrict) where T : class, IOrganization
        {
            builder.HasOne(model => model.Organization)
                .WithMany()
                .OnDelete(deleteBehavior)
                .HasForeignKey(model => model.OrganizationId)
                .IsRequired();

            builder.HasIndex(model => model.OrganizationId)
                .ForSqlServerIsClustered(false)
                .HasName($"IX_{nameof(IOrganization.OrganizationId)}");
        }

    }
}
