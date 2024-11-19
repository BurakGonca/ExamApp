namespace BAExamApp.Entities.Configurations;

public class QuestionRevisionConfiguration:AuditableEntityConfiguration<QuestionRevision>
{
    public override void Configure(EntityTypeBuilder<QuestionRevision> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.RequestDescription).HasMaxLength(256);
        builder.Property(x => x.RevisionConclusion).HasMaxLength(256);

        builder.HasOne(x => x.Question).WithMany(x => x.QuestionRevisions).HasForeignKey(x => x.QuestionId);
        builder.HasOne(x => x.RequesterAdmin).WithMany(x => x.QuestionRevisions).HasForeignKey(x => x.RequesterAdminId);
        builder.HasOne(x => x.RequestedTrainer).WithMany(x => x.QuestionRevisions).HasForeignKey(x => x.RequestedTrainerId);
    }
}