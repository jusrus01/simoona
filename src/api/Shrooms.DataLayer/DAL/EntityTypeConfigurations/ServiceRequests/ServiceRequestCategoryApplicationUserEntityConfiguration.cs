using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.EntityTypeConfigurations.ServiceRequests
{
    public class ServiceRequestCategoryApplicationUserEntityConfiguration : IEntityTypeConfiguration<ServiceRequestCategoryApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestCategoryApplicationUser> builder)
        {
            builder.ToTable("ServiceRequestCategoryApplicationUsers");

            builder.Property(model => model.ApplicationUserId)
                .HasColumnName("ApplicationUser_Id");

            builder.Property(model => model.ServiceRequestCategoryId)
                .HasColumnName("ServiceRequestCategory_Id");

            builder.HasKey(model => new { model.ServiceRequestCategoryId, model.ApplicationUserId })
                .HasName("PK_dbo.ServiceRequestCategoryApplicationUsers");

            builder.HasOne(model => model.ApplicationUser)
                .WithMany(model => model.ServiceRequestCategoryApplicationUsers)
                .HasForeignKey(model => model.ApplicationUserId)
                .HasConstraintName("FK_dbo.ServiceRequestCategoryApplicationUsers_dbo.AspNetUsers_ApplicationUser_Id")
                .IsRequired();

            builder.HasOne(model => model.ServiceRequestCategory)
                .WithMany(model => model.ServiceRequestCategoryApplicationUsers)
                .HasForeignKey(model => model.ServiceRequestCategoryId)
                .HasConstraintName("FK_dbo.ServiceRequestCategoryApplicationUsers_dbo.ServiceRequestCategories_ServiceRequestCategory_Id")
                .IsRequired();

            builder.HasIndex(model => model.ApplicationUserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUser_Id");

            builder.HasIndex(model => model.ServiceRequestCategoryId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ServiceRequestCategory_Id");
        }
    }
}
