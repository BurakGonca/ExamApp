namespace BAExamApp.Entities.Configurations;

public class ExamClassroomsConfiguration : AuditableEntityConfiguration<ExamClassrooms>
{
    public override void Configure(EntityTypeBuilder<ExamClassrooms> builder)
    {
        base.Configure(builder);
        
        builder.HasOne(x => x.Exam)
            .WithMany(x => x.ExamClassrooms)
            .HasForeignKey(x => x.ExamId);
        
        builder.HasOne(x => x.Classroom)
            .WithMany(x => x.ExamClassrooms)
            .HasForeignKey(x => x.ClassroomId);

        builder.HasAlternateKey(x => new { x.ExamId, x.ClassroomId });
    }
}
