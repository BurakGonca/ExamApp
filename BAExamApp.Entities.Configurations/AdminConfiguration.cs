namespace BAExamApp.Entities.Configurations;

public class AdminConfiguration : BaseUserConfiguration<Admin>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {
        base.Configure(builder);

        //builder.HasOne(x => x.City).WithMany(x => x.Admins).HasForeignKey(x => x.CityId);

    }
}
