namespace BAExamApp.Entities.Configurations;

public class ExamRuleConfiguration : AuditableEntityConfiguration<ExamRule>
{
    public override void Configure(EntityTypeBuilder<ExamRule> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        

        builder.HasOne(x => x.Product).WithMany(x => x.ExamRules).HasForeignKey(x => x.ProductId);

        builder.Property(x => x.Description).IsRequired(false);
    }
}