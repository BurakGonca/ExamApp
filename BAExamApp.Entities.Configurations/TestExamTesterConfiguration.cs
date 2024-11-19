namespace BAExamApp.Entities.Configurations;
public class TestExamTesterConfiguration : BaseEntityConfiguration<TestExamTester>
{
    public override void Configure(EntityTypeBuilder<TestExamTester> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.TestExam).WithMany(x => x.TestExamTesters).HasForeignKey(x => x.TestExamId);
        builder.HasOne(x => x.Tester).WithMany(x => x.TestExamTesters).HasForeignKey(x => x.TesterId);
    }
}