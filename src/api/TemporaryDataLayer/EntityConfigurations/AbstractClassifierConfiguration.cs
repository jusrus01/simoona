﻿//namespace TemporaryDataLayer
//{
//    internal class AbstractClassifierConfiguration : EntityTypeConfiguration<AbstractClassifier>
//    {
//        public AbstractClassifierConfiguration()
//        {
//            Map<Language>(m =>
//            {
//                m.Requires("ClassificatorType").HasValue("Language");
//                m.Requires("IsDeleted").HasValue(false);
//            });

//            Map<Certificate>(m =>
//            {
//                m.Requires("ClassificatorType").HasValue("Certificate");
//                m.Requires("IsDeleted").HasValue(false);
//            });

//            HasRequired(a => a.Organization)
//                .WithMany()
//                .HasForeignKey(a => a.OrganizationId)
//                .WillCascadeOnDelete(false);
//        }
//    }
//}
