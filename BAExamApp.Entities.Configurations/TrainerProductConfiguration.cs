namespace BAExamApp.Entities.Configurations;

public class TrainerProductConfiguration : BaseEntityConfiguration<TrainerProduct>
{
    public override void Configure(EntityTypeBuilder<TrainerProduct> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Trainer).WithMany(x => x.TrainerProducts).HasForeignKey(x => x.TrainerId);
        builder.HasOne(x => x.Product).WithMany(x => x.TrainerProducts).HasForeignKey(x => x.ProductId);

        builder.HasAlternateKey(x => new { x.ProductId, x.TrainerId });
    }
}
