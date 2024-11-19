namespace BAExamApp.Entities.Configurations;

public class TechnicalUnitConfiguration : AuditableEntityConfiguration<TechnicalUnit>
{
    public override void Configure(EntityTypeBuilder<TechnicalUnit> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(512).IsRequired();
    }
}
