using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations;

public class StudentExamConfiguration : AuditableEntityConfiguration<StudentExam>
{
    public override void Configure(EntityTypeBuilder<StudentExam> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Score).IsRequired(false).HasColumnType("Decimal");
        builder.Property(x => x.IsFinished).HasColumnType("bool").IsRequired();
        builder.Property(x => x.AnsweredQuestionCount).IsRequired();

        builder.HasOne(x => x.Exam).WithMany(x => x.StudentExams).HasForeignKey(x => x.ExamId);
        builder.HasOne(x => x.Student).WithMany(x => x.StudentExams).HasForeignKey(x => x.StudentId);
        builder.HasOne(x => x.Evaluator).WithMany(x => x.StudentExams).HasForeignKey(x => x.EvaluatorId);

        builder.HasAlternateKey(x => new { x.ExamId, x.StudentId });
    }
}