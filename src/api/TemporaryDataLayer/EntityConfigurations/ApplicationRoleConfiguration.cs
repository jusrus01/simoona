﻿namespace TemporaryDataLayer
{
    internal class ApplicationRoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleConfiguration()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false))
                .ToTable("AspNetRoles");

            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256);

            HasMany(r => r.Users)
                .WithRequired()
                .HasForeignKey(ur => ur.RoleId);

            HasRequired(r => r.Organization)
                .WithMany()
                .HasForeignKey(r => r.OrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}
