namespace TemporaryDataLayer
{
    public class BlacklistUserEntityConfig : EntityTypeConfiguration<BlacklistUser>
    {
        public BlacklistUserEntityConfig()
        {
            HasRequired(u => u.ModifiedByUser)
                .WithMany()
                .HasForeignKey(u => u.ModifiedBy)
                .WillCascadeOnDelete(false);

            HasRequired(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedBy)
                .WillCascadeOnDelete(false);

            Property(u => u.Reason)
                .IsOptional();

            Property(u => u.UserId)
                .IsRequired();

            Property(u => u.EndDate)
                .IsRequired();

            Property(u => u.Status)
                .IsRequired();

            Property(u => u.ModifiedBy)
                .IsRequired();
        }
    }
}
