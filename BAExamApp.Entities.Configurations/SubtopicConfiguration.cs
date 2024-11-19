namespace BAExamApp.Entities.Configurations;

public class SubtopicConfiguration : AuditableEntityConfiguration<Subtopic>
{
    public override void Configure(EntityTypeBuilder<Subtopic> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

        builder.HasMany(x => x.QuestionSubtopics).WithOne(x => x.Subtopic).HasForeignKey(x => x.SubtopicId);
    }
}
