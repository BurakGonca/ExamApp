namespace BAExamApp.Entities.Configurations;
public class EmailConfiguration : AuditableEntityConfiguration<Email>
{
    public override void Configure(EntityTypeBuilder<Email> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EmailAddress).IsRequired();

        builder.Property(x => x.IdentityId).IsRequired();
    }
}
