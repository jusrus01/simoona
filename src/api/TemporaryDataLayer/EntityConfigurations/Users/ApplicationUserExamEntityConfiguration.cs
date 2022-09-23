using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryDataLayer.Models.Users;

namespace TemporaryDataLayer.EntityConfigurations.Users
{
    public class ApplicationUserExamEntityConfiguration : IEntityTypeConfiguration<ApplicationUserExam>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserExam> builder)
        {
            builder.ToTable("ApplicationUserExams");

            builder.HasKey(model => new { model.ApplicationUserId, model.ExamId })
                .HasName("PK_dbo.ApplicationUserExams");

            builder.HasOne(model => model.ApplicationUser)
                .WithMany(model => model.ApplicationUserExams)
                .HasForeignKey(model => model.ApplicationUserId)
                .HasConstraintName("FK_dbo.ApplicationUserExams_dbo.AspNetUsers_ApplicationUserId")
                .IsRequired();

            builder.HasOne(model => model.Exam)
                .WithMany(model => model.ApplicationUserExams)
                .HasForeignKey(model => model.ExamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.ApplicationUserExams_dbo.Exams_ExamId")
                .IsRequired();

            builder.HasIndex(model => model.ApplicationUserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUserId");

            builder.HasIndex(model => model.ExamId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ExamId");
        }
    }
}
