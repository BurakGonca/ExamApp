using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations;
public class TestExamConfiguration : AuditableEntityConfiguration<TestExam>
{
    public override void Configure(EntityTypeBuilder<TestExam> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.TestExamDate).HasColumnType("date").IsRequired();
        builder.Property(x => x.TestExamDuration).HasColumnType("time").IsRequired();
        builder.Property(x => x.Description).IsRequired(false);
    }
}
