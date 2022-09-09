//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace TemporaryDataLayer.EntityConfigurations
//{
//    internal class ExamEntityConfig : IEntityTypeConfiguration<Exam>
//    {
//        _modelBuilder.Entity<Exam>()
//                .HasRequired(a => a.Organization)
//                .WithMany()
//                .HasForeignKey(a => a.OrganizationId)
//                .WillCascadeOnDelete(false);


//        public void Configure(EntityTypeBuilder<Exam> builder)
//        {
//            builder.Property(model => model.Organization)
//                .
//        }
//    }
//}
