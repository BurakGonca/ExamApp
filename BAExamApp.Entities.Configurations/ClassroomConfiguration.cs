using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations;

public class ClassroomConfiguration : AuditableEntityConfiguration<Classroom>
{
    public override void Configure(EntityTypeBuilder<Classroom> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.OpeningDate).HasColumnType("date").IsRequired();
        builder.Property(x => x.ClosedDate).HasColumnType("date").IsRequired();

        builder.HasOne(x => x.GroupType).WithMany(x => x.Classrooms).HasForeignKey(x => x.GroupTypeId);
        builder.HasOne(x => x.Branch).WithMany(x => x.Classrooms).HasForeignKey(x => x.BranchId);

        builder.HasMany(x => x.ExamClassrooms)
            .WithOne(ec => ec.Classroom)
            .HasForeignKey(ec => ec.ClassroomId);
    }
}
