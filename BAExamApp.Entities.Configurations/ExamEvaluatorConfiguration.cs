namespace BAExamApp.Entities.Configurations;

public class ExamEvaluatorConfiguration : BaseEntityConfiguration<ExamEvaluator>
{
    public override void Configure(EntityTypeBuilder<ExamEvaluator> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Exam).WithMany(x => x.ExamEvaluators).HasForeignKey(x => x.ExamId);
        builder.HasOne(x => x.Trainer).WithMany(x => x.ExamEvaluators).HasForeignKey(x => x.TrainerId);

        builder.HasAlternateKey(x => new { x.ExamId, x.TrainerId, });
    }
}
