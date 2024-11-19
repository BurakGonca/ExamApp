using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations;

public class ExamConfiguration : AuditableEntityConfiguration<Exam>
{
    public override void Configure(EntityTypeBuilder<Exam> builder)
    {
        base.Configure(builder);
                
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.ExamDateTime).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.ExamDuration).HasColumnType("time").IsRequired();
        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.ExamType).IsRequired();
        builder.Property(x => x.ExamCreationType).IsRequired();
        builder.Property(x => x.MaxScore).IsRequired();
        builder.Property(x => x.BonusScore).IsRequired();

        builder.HasOne(x => x.ExamRule).WithMany(x => x.Exams).HasForeignKey(x => x.ExamRuleId);
        builder.HasMany(x => x.ExamClassrooms).WithOne(ec => ec.Exam).HasForeignKey(ec => ec.ExamId);
    }
}