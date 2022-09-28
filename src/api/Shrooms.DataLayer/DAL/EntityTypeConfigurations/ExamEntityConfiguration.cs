using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class ExamEntityConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();

            builder.AddSoftDelete()
                .HasDefaultValue(null);

            builder.Property(model => model.Title)
                .HasMaxLength(ValidationConstants.ExamMaxTitleLength)
                .IsRequired();

            builder.Property(model => model.Number)
                .HasMaxLength(ValidationConstants.ExamMaxNumberLength);

            builder.HasIndex(model => model.Title)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Title");
            
            builder.HasIndex(model => model.Number)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Number");
        }
    }
}
