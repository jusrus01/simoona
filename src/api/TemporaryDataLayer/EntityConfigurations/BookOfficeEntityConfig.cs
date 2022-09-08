﻿namespace TemporaryDataLayer
{
    internal class BookOfficeEntityConfig : EntityTypeConfiguration<BookOffice>
    {
        public BookOfficeEntityConfig()
        {
            Map(e => e.Requires("IsDeleted")
                .HasValue(false));

            HasRequired(u => u.Book)
                .WithMany(x => x.BookOffices)
                .HasForeignKey(x => x.BookId)
                .WillCascadeOnDelete(true);

            HasRequired(u => u.Office)
                .WithMany(x => x.BookOffices)
                .HasForeignKey(x => x.OfficeId)
                .WillCascadeOnDelete(false);

            HasRequired(u => u.Organization)
                .WithMany()
                .HasForeignKey(x => x.OrganizationId)
                .WillCascadeOnDelete(false);

            Property(u => u.ModifiedBy)
                .HasMaxLength(50);

            Property(u => u.CreatedBy)
                .HasMaxLength(50);

            Property(u => u.BookId)
               .HasColumnAnnotation(
                   IndexAnnotation.AnnotationName,
                   new IndexAnnotation(
                       new IndexAttribute("BookId_OfficeId", 1) { IsUnique = true }));

            Property(u => u.OfficeId)
               .HasColumnAnnotation(
                   IndexAnnotation.AnnotationName,
                   new IndexAnnotation(
                       new IndexAttribute("BookId_OfficeId", 2) { IsUnique = true }));
        }
    }
}
