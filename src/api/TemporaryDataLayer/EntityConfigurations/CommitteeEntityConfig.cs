﻿namespace TemporaryDataLayer
{
    internal class CommitteeEntityConfig : EntityTypeConfiguration<Committee>
    {
        public CommitteeEntityConfig()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));
            HasMany(a => a.Suggestions)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("Committees_Id");
                    x.MapRightKey("CommitteeSuggestions_Id");
                    x.ToTable("CommitteeSuggestionsIDs");
                });

            HasRequired(c => c.Organization)
                .WithMany()
                .HasForeignKey(c => c.OrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}
