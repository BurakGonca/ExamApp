namespace BAExamApp.Entities.Configurations;

public class QuestionConfiguration : AuditableEntityConfiguration<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.State).IsRequired();
        builder.Property(x => x.QuestionType).IsRequired();
        builder.Property(x => x.Image).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.Target).IsRequired(false);
        builder.Property(x => x.Gains).IsRequired(false);

        builder.HasMany(x => x.QuestionSubtopics).WithOne(x => x.Question).HasForeignKey(x => x.QuestionId);
        builder.HasOne(x => x.QuestionDifficulty).WithMany(x => x.Questions).HasForeignKey(x => x.QuestionDifficultyId);
    }
}
