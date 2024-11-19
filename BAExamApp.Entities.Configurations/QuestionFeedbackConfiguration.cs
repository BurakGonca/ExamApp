namespace BAExamApp.Entities.Configurations;
public class QuestionFeedbackConfiguration : AuditableEntityConfiguration<QuestionFeedback>
{
    public override void Configure(EntityTypeBuilder<QuestionFeedback> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.LikeStatus).IsRequired(false);
        builder.Property(x => x.Comment).HasMaxLength(512);

        builder.HasOne(x => x.Question).WithMany(x => x.QuestionFeedbacks).HasForeignKey(x => x.QuestionId);
    }
}
