using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using System;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class SkillEntityConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Title)
                .HasMaxLength(200)
                .IsRequired();
            
            builder.HasIndex(model => model.Title)
                .ForSqlServerIsClustered(false);

            builder.Property(model => model.ShowInAutoComplete)
                .HasDefaultValue(false);

            builder.HasIndex(model => model.Title)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Title");
        }
    }
}
