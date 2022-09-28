using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shrooms.DataLayer.DAL
{
    public static class EntityTypeBuilderExtensions
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

        // TODO: replace this with .HasMany().WithMany() after upgrading to EF Core 5+
        public static void ConfigureManyToMany<T, TFirstEntity, TSecondEntity>(
            this EntityTypeBuilder<T> builder,
            Expression<Func<T, object>> key,
            string partialKeyName,
            Expression<Func<TFirstEntity, IEnumerable<T>>> firstWithMany,
            Expression<Func<T, object>> firstForeignKey,
            Expression<Func<T, TFirstEntity>> firstObject,
            string partialFirstForeignKeyName,
            string firstForeignKeyConstraintName,
            Expression<Func<TSecondEntity, IEnumerable<T>>> secondWithMany,
            Expression<Func<T, object>> secondForeignKey,
            Expression<Func<T, TSecondEntity>> secondObject,
            string partialSecondForeignKeyName,
            string secondForeignKeyConstraintName,
            bool includeUnderscore = true) 
            where T : class
            where TFirstEntity : class
            where TSecondEntity : class
        {
            var idPrefix = includeUnderscore ? "_Id" : "Id";

            builder.Property(firstForeignKey)
                .HasColumnName($"{partialFirstForeignKeyName}{idPrefix}");

            builder.Property(secondForeignKey)
                .HasColumnName($"{partialSecondForeignKeyName}{idPrefix}");

            builder.HasKey(key)
                .HasName($"PK_dbo.{partialKeyName}");

            builder.HasOne(firstObject)
                .WithMany(firstWithMany)
                .HasForeignKey(firstForeignKey)
                .HasConstraintName(firstForeignKeyConstraintName)
                .IsRequired();

            builder.HasOne(secondObject)
                .WithMany(secondWithMany)
                .HasForeignKey(secondForeignKey)
                .HasConstraintName(secondForeignKeyConstraintName)
                .IsRequired();

            builder.HasIndex(firstForeignKey)
                .ForSqlServerIsClustered(false)
                .HasName($"IX_{partialFirstForeignKeyName}{idPrefix}");

            builder.HasIndex(secondForeignKey)
                .ForSqlServerIsClustered(false)
                .HasName($"IX_{partialSecondForeignKeyName}{idPrefix}");
        }

        public static void AddDefaultBaseModelConfiguration<T>(this EntityTypeBuilder<T> builder, bool hasDefaultValue = false) where T : BaseModel
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

        public static void AddOrganization<T>(
            this EntityTypeBuilder<T> builder, 
            DeleteBehavior deleteBehavior = DeleteBehavior.Restrict,
            string foreignKeyName = null) where T : class, IOrganization
        {
            var orgCollectionBuilder = builder.HasOne(model => model.Organization)
                .WithMany()
                .OnDelete(deleteBehavior)
                .HasForeignKey(model => model.OrganizationId)
                .IsRequired();

            if (foreignKeyName != null)
            {
                orgCollectionBuilder.HasConstraintName(foreignKeyName);
            }

            builder.HasIndex(model => model.OrganizationId)
                .ForSqlServerIsClustered(false)
                .HasName($"IX_{nameof(IOrganization.OrganizationId)}");
        }
    }
}
