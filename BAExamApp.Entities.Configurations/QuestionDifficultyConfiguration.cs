using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations;

public class QuestionDifficultyConfiguration : AuditableEntityConfiguration<QuestionDifficulty>
{
    public override void Configure(EntityTypeBuilder<QuestionDifficulty> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.TimeGiven).HasColumnType("time").IsRequired();
        builder.Property(x => x.ScoreCoefficient).HasColumnType("float").IsRequired();
    }
}
