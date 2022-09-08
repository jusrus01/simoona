﻿namespace TemporaryDataLayer
{
    internal class PostEntityConfig : EntityTypeConfiguration<Post>
    {
        public PostEntityConfig()
        {
            Property(x => x.LastActivity)
                .HasColumnType("datetime2")
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("IX_LastActivity")
                    {
                        IsUnique = false,
                        IsClustered = false
                    }))
                ;
        }
    }
}
