namespace BAExamApp.Entities.Configurations;

public class GroupTypeConfiguration : AuditableEntityConfiguration<GroupType>
{
    public override void Configure(EntityTypeBuilder<GroupType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Information).IsRequired();
    }
}