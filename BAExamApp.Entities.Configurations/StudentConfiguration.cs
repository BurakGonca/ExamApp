namespace BAExamApp.Entities.Configurations;

public class StudentConfiguration : BaseUserConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        //builder.HasOne(x => x.City).WithMany(x => x.Students).HasForeignKey(x => x.CityId);
        builder.Property(x => x.IdentityId).IsRequired(false);
    }
}
