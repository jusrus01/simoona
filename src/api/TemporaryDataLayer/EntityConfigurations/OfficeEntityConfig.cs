//namespace TemporaryDataLayer
//{
//    internal class OfficeEntityConfig : EntityTypeConfiguration<Office>
//    {
//        public OfficeEntityConfig()
//        {
//            Map(e => e.Requires("IsDeleted").HasValue(false));

//            HasRequired(o => o.Organization)
//                .WithMany()
//                .HasForeignKey(o => o.OrganizationId)
//                .WillCascadeOnDelete(false);
//        }
//    }
//}
