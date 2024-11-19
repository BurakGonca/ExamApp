namespace BAExamApp.Entities.Configurations;

public class ProductSubjectConfiguration : BaseEntityConfiguration<ProductSubject>
{
    public override void Configure(EntityTypeBuilder<ProductSubject> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Product).WithMany(x => x.ProductSubjects).HasForeignKey(x => x.ProductId);
        builder.HasOne(x => x.Subject).WithMany(x => x.ProductSubjects).HasForeignKey(x => x.SubjectId);

        builder.HasAlternateKey(x => new { x.ProductId, x.SubjectId });
    }
}