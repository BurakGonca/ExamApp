namespace BAExamApp.Entities.Configurations;

public class StudentAnswerConfiguration : BaseEntityConfiguration<StudentAnswer>
{
    public override void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.StudentQuestion).WithMany(x => x.StudentAnswers).HasForeignKey(x => x.StudentQuestionId);
        builder.HasOne(x => x.QuestionAnswer).WithMany(x => x.StudentAnswers).HasForeignKey(x => x.QuestionAnswerId);

        builder.HasAlternateKey(x => new { x.StudentQuestionId, x.QuestionAnswerId });
    }
}
