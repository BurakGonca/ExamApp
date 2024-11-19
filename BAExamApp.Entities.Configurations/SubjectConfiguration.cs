namespace BAExamApp.Entities.Configurations;

public class SubjectConfiguration : AuditableEntityConfiguration<Subject>
{
    public override void Configure(EntityTypeBuilder<Subject> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();
    }
}
