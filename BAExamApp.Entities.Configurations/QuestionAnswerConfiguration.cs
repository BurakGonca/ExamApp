namespace BAExamApp.Entities.Configurations;

public class QuestionAnswerConfiguration : BaseEntityConfiguration<QuestionAnswer>
{
    public override void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Answer).IsRequired();
        builder.Property(x => x.IsRightAnswer).IsRequired();
        builder.Property(x => x.IsAnswerImage).IsRequired();

        builder.HasOne(x => x.Question).WithMany(x => x.QuestionAnswers).HasForeignKey(x => x.QuestionId);
    }
}