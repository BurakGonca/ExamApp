namespace BAExamApp.Entities.Configurations;

public class StudentQuestionConfiguration : AuditableEntityConfiguration<StudentQuestion>
{
    public override void Configure(EntityTypeBuilder<StudentQuestion> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.MaxScore).IsRequired();
        builder.Property(x => x.BonusScore).IsRequired();
        builder.Property(x => x.Score).IsRequired(false);
        builder.Property(x => x.QuestionOrder).IsRequired();
        builder.Property(x => x.TimeStarted).IsRequired(false);
        builder.Property(x => x.TimeFinished).IsRequired(false);

        builder.HasOne(x => x.StudentExam).WithMany(x => x.StudentQuestions).HasForeignKey(x => x.StudentExamId);
        builder.HasOne(x => x.Question).WithMany(x => x.StudentQuestions).HasForeignKey(x => x.QuestionId);

        //builder.HasAlternateKey(x => new { x.StudentExamId, x.QuestionId });
    }
}
