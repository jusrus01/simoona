﻿namespace TemporaryDataLayer
{
    internal class BadgeCategoryEntityConfiguration : EntityTypeConfiguration<BadgeCategory>
    {
        public BadgeCategoryEntityConfiguration()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));

            Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(u => u.Description)
                .HasMaxLength(4000);

            Property(u => u.ModifiedBy)
                .HasMaxLength(50);

            Property(u => u.CreatedBy)
                .HasMaxLength(50);

            HasMany(x => x.RelationshipsWithKudosTypes)
                .WithRequired()
                .HasForeignKey(x => x.BadgeCategoryId)
                .WillCascadeOnDelete(value: false);

            HasMany(x => x.BadgeTypes)
                .WithRequired()
                .HasForeignKey(x => x.BadgeCategoryId)
                .WillCascadeOnDelete(value: true);
        }
    }
}
