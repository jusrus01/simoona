using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    internal static class Extensions
    {
        private const string IsDeleted = nameof(IsDeleted); // TODO: save somewhere

        internal static PropertyBuilder AddSoftDelete<T>(this EntityTypeBuilder<T> builder) where T : class
        {
            return builder.Property(typeof(bool), IsDeleted);
                //.HasDefaultValue(false);
        }

        internal static void AddDefaultBaseModelConfiguration<T>(this EntityTypeBuilder<T> builder) where T : BaseModel
        {
            builder.Property(model => model.Created)
                .HasColumnType("datetime");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime");
        }

        internal static void MapRequiredOrganization<T>(this EntityTypeBuilder<T> builder, DeleteBehavior deleteBehavior = DeleteBehavior.Restrict) where T : class, IOrganization
        {
            builder.HasOne(model => model.Organization)
                .WithMany()
                .OnDelete(deleteBehavior)
                .HasForeignKey(model => model.OrganizationId)
                .IsRequired();

            builder.HasIndex(model => model.OrganizationId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_OrganizationId");
        }
    }
}
