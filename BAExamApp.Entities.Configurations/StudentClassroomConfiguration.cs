namespace BAExamApp.Entities.Configurations;

public class StudentClassroomConfiguration : AuditableEntityConfiguration<StudentClassroom>
{
    public override void Configure(EntityTypeBuilder<StudentClassroom> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Student).WithMany(x => x.StudentClassrooms).HasForeignKey(x => x.StudentId);
        builder.HasOne(x => x.Classroom).WithMany(x => x.StudentClassrooms).HasForeignKey(x => x.ClassroomId);

        builder.HasAlternateKey(x => new { x.ClassroomId, x.StudentId });
    }
}
