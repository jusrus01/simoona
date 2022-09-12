using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        //public ApplicationUserConfiguration()
        //{
        //    HasMany(u => u.Exams)
        //        .WithMany(e => e.ApplicationUsers)
        //        .Map(t => t.MapLeftKey("ApplicationUserId")
        //                .MapRightKey("ExamId")
        //                .ToTable("ApplicationUserExams"));

        //    HasMany(u => u.Skills)
        //        .WithMany(s => s.ApplicationUsers)
        //        .Map(t => t.MapLeftKey("ApplicationUserId")
        //            .MapRightKey("SkillId")
        //            .ToTable("ApplicationUserSkills"));

        //    HasMany(u => u.ManagedUsers)
        //        .WithOptional()
        //        .HasForeignKey(u => u.ManagerId);

        //    HasMany(u => u.Roles)
        //        .WithRequired()
        //        .HasForeignKey(ur => ur.UserId);

        //    HasMany(u => u.Claims)
        //        .WithRequired()
        //        .HasForeignKey(uc => uc.UserId);

        //    HasMany(u => u.Logins)
        //        .WithRequired()
        //        .HasForeignKey(ul => ul.UserId);

        //    HasOptional(u => u.WorkingHours)
        //        .WithRequired(w => w.ApplicationUser)
        //        .Map(m => m.MapKey("ApplicationUserId"));

        //    HasMany(e => e.Events)
        //        .WithRequired()
        //        .HasForeignKey(e => e.ResponsibleUserId)
        //        .WillCascadeOnDelete(value: false);

        //    HasRequired(u => u.Organization)
        //        .WithMany()
        //        .HasForeignKey(u => u.OrganizationId)
        //        .WillCascadeOnDelete(value: false);

        //    HasMany(u => u.OwnedProjects)
        //        .WithRequired()
        //        .HasForeignKey(p => p.OwnerId)
        //        .WillCascadeOnDelete(value: false);

        //    HasMany(u => u.Committees)
        //        .WithMany(c => c.Members)
        //        .Map(x =>
        //        {
        //            x.ToTable("CommitteesUsersMembership");
        //        });

        //    HasMany(u => u.DelegatingCommittees)
        //        .WithMany(c => c.Delegates)
        //        .Map(x =>
        //        {
        //            x.ToTable("CommitteesUsersDelegates");
        //        });

        //    HasMany(u => u.LeadingCommittees)
        //        .WithMany(c => c.Leads)
        //        .Map(x =>
        //        {
        //            x.ToTable("CommitteesUsersLeadership");
        //        });

        //    HasOptional(u => u.NotificationsSettings)
        //        .WithOptionalPrincipal(s => s.ApplicationUser);
        //}

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.AddSoftDelete();
            builder.MapRequiredOrganization();

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
        }
    }
}
