namespace BAExamApp.Entities.Configurations;
public class ExamRuleSubtopicConfiguration : BaseEntityConfiguration<ExamRuleSubtopic>
{
    public override void Configure(EntityTypeBuilder<ExamRuleSubtopic> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ExamType).IsRequired();
        builder.Property(x => x.QuestionType).IsRequired();
        builder.Property(x => x.QuestionCount).IsRequired();

        builder.HasOne(x => x.ExamRule).WithMany(x => x.ExamRuleSubtopics).HasForeignKey(x => x.ExamRuleId);
        builder.HasOne(x => x.Subtopic).WithMany(x => x.ExamRuleSubtopics).HasForeignKey(x => x.SubtopicId);
        builder.HasOne(x => x.QuestionDifficulty).WithMany(x => x.ExamRuleSubtopics).HasForeignKey(x => x.QuestionDifficultyId);

        builder.HasAlternateKey(x => new { x.ExamRuleId, x.SubtopicId, x.ExamType, x.QuestionType, x.QuestionDifficultyId });
    }
}
