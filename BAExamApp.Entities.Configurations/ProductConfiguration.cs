namespace BAExamApp.Entities.Configurations;

public class ProductConfiguration : AuditableEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(512).IsRequired();
        builder.Property(x => x.IsActive).IsRequired(false);

        builder.HasOne(x => x.TechnicalUnit).WithMany(x => x.Products).HasForeignKey(x => x.TechnicalUnitId);
    }
}