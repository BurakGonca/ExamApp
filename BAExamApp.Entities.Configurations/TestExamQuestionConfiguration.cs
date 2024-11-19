using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations;
public class TestExamQuestionConfiguration : AuditableEntityConfiguration<TestExamQuestion>
{
    public override void Configure(EntityTypeBuilder<TestExamQuestion> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.TestExam).WithMany(x => x.TestExamQuestions).HasForeignKey(x => x.TestExamId);
        builder.HasOne(x => x.Question).WithMany(x => x.TestExamsQuestions).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.NoAction);
    }
}
