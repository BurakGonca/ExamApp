namespace BAExamApp.Entities.Configurations;

public class TrainerConfiguration : BaseUserConfiguration<Trainer>
{
    public override void Configure(EntityTypeBuilder<Trainer> builder)
    {
        base.Configure(builder);

        //builder.HasOne(x => x.TechnicalUnit).WithMany(x => x.Trainers).HasForeignKey(x => x.TechnicalUnitId);
        //builder.HasOne(x => x.City).WithMany(x => x.Trainers).HasForeignKey(x => x.CityId);
    }
}
