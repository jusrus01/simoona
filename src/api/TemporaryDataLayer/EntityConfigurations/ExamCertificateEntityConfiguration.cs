using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class ExamCertificateEntityConfiguration : IEntityTypeConfiguration<ExamCertificate>
    {
        public void Configure(EntityTypeBuilder<ExamCertificate> builder)
        {
            builder.ToTable("ExamCertificates");

            builder.Property(model => model.CertificateId)
                .HasColumnName("Certificate_Id");

            builder.Property(model => model.ExamId)
                .HasColumnName("Exam_Id");

            builder.HasKey(model => new { model.ExamId, model.CertificateId })
                .HasName("PK_dbo.ExamCertificates"); // TODO: fix Class1

            builder.HasOne(model => model.Certificate)
                .WithMany(model => model.ExamCertificates)
                .HasForeignKey(model => model.CertificateId)
                .HasConstraintName("FK_dbo.ExamCertificates_dbo.AbstractClassifiers_Certificate_Id")
                .IsRequired();

            builder.HasOne(model => model.Exam)
                .WithMany(model => model.ExamCertificates)
                .HasForeignKey(model => model.ExamId)
                .HasConstraintName("FK_dbo.ExamCertificates_dbo.Exams_Exam_Id")
                .IsRequired();

            builder.HasIndex(model => model.CertificateId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Certificate_Id");

            builder.HasIndex(model => model.ExamId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Exam_Id");
        }
    }
}
