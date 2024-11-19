namespace BAExamApp.Entities.Configurations;

public class BranchConfiguration : AuditableEntityConfiguration<Branch>
{
    public override void Configure(EntityTypeBuilder<Branch> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
    }
}