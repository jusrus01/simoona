using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using System;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Users
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddOrganization();

            builder.Ignore(model => model.FullName);
            builder.Ignore(model => model.YearsEmployed);

            builder.Property(model => model.IsAnonymized)
                .IsRequired();

            builder.Property(model => model.UserName)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(model => model.Email)
                .IsUnique();

            builder.Property(model => model.Email)
                .HasMaxLength(256);

            builder.Property(model => model.IsManagingDirector)
                .IsRequired();

            builder.Ignore(model => model.UserWasPreviouslyBlacklisted);
            builder.Ignore(model => model.YearsEmployed);

            builder.HasMany<IdentityUserClaim<string>>()
                .WithOne()
                .HasForeignKey(model => model.UserId)
                .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");

            builder.HasMany<IdentityUserLogin<string>>()
                .WithOne()
                .HasForeignKey(model => model.UserId)
                .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");

            builder.HasMany(model => model.BlacklistEntries)
                .WithOne(model => model.User)
                .HasForeignKey(model => model.UserId);

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Id)
                .HasMaxLength(128);

            builder.Property(model => model.Created)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.Modified)
               .HasColumnType("datetime")
               .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.HasMany(model => model.BadgeLogs)
                .WithOne(model => model.Employee)
                .HasForeignKey(model => model.EmployeeId);

            builder.Property(typeof(DateTime?), "LockoutEndDateUtc")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(model => model.EmploymentDate)
                .HasColumnType("datetime");

            builder.Property(model => model.IsAbsent)
                .HasDefaultValue(false);

            builder.Property(model => model.IsOwner)
                .HasDefaultValue(false);

            builder.Property(model => model.Created)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.BirthDay)
                .HasColumnType("datetime");

            builder.Property(model => model.TotalKudos)
                .HasDefaultValue(0);

            builder.Property(model => model.RemainingKudos)
                .HasDefaultValue(0);

            builder.Property(model => model.RemainingKudos)
                .HasDefaultValue(0);

            builder.Property(model => model.VacationLastTimeUpdated)
                .HasColumnType("datetime");

            builder.Property(model => model.SittingPlacesChanged)
                .HasDefaultValue(0);

            builder.Property(model => model.IsManagingDirector)
                .HasDefaultValue(false);

            builder.Property(model => model.DailyMailingHour);

            builder.Property(model => model.CultureCode)
                .HasDefaultValue("en-US");

            builder.Property(model => model.TimeZone)
                .HasDefaultValue("FLE Standard Time");

            builder.Property(model => model.IsTutorialComplete)
                .HasDefaultValue(true);

            builder.Property(model => model.IsAnonymized)
                .HasDefaultValue(false);

            builder.Property(model => model.SpentKudos)
                .HasDefaultValue(0);

            builder.HasIndex(model => model.Email)
                .ForSqlServerIsClustered(false)
                .IsUnique()
                .HasFilter(null)
                .HasName("Email");

            // Not sure if this is correct,
            // however this drops the additional ApplicationUserId
            builder.HasMany(model => model.Events)
                .WithOne(model => model.ResponsibleUser)
                .HasForeignKey(model => model.ResponsibleUserId);
        }
    }
}
