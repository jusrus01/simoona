﻿namespace TemporaryDataLayer
{
    internal class WallConfiguration : EntityTypeConfiguration<Wall>
    {
        public WallConfiguration()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));

            HasRequired(x => x.Organization)
               .WithMany()
               .WillCascadeOnDelete(false);
        }
    }
}
