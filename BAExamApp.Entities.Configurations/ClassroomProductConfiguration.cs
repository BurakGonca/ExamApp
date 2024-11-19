namespace BAExamApp.Entities.Configurations;
public class ClassroomProductConfiguration : AuditableEntityConfiguration<ClassroomProduct>
{
    public override void Configure(EntityTypeBuilder<ClassroomProduct> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Product).WithMany(x => x.ClassroomProducts).HasForeignKey(x => x.ProductId);
        builder.HasOne(x => x.Classroom).WithMany(x => x.ClassroomProducts).HasForeignKey(x => x.ClassroomId);

        builder.HasAlternateKey(x => new { x.ProductId, x.ClassroomId });
    }
}
