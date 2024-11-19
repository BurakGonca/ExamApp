namespace BAExamApp.Entities.Configurations;

public class TrainerClassroomConfiguration : BaseEntityConfiguration<TrainerClassroom>
{
    public override void Configure(EntityTypeBuilder<TrainerClassroom> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.AssignedDate).IsRequired();
        builder.Property(x => x.ResignedDate).IsRequired(false);

        builder.HasOne(x => x.Trainer).WithMany(x => x.TrainerClassrooms).HasForeignKey(x => x.TrainerId);
        builder.HasOne(x => x.Classroom).WithMany(x => x.TrainerClassrooms).HasForeignKey(x => x.ClassroomId);

        builder.HasAlternateKey(x => new { x.ClassroomId, x.TrainerId });
    }
}
